﻿using ImGuiNET;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Immutable;
using Murder.Editor.Attributes;
using Murder.Editor.ImGuiExtended;
using Murder.Editor.Reflection;
using Murder.Editor.Utilities;
using Murder.Core.Graphics;
using Murder.Assets;
using Murder.Editor.Stages;
using Murder.Diagnostics;
using Murder.Editor.Utilities.Attributes;
using Bang.Entities;
using Murder.Editor.Components;
using Murder.Editor.Systems;

namespace Murder.Editor.CustomEditors
{
    [CustomEditorOf(typeof(CharacterAsset))]
    public partial class CharacterEditor : CustomEditor
    {
        /// <summary>
        /// Tracks the dialog system editors across different guids.
        /// </summary>
        protected Dictionary<Guid, Stage> Stages { get; private set; } = new();

        private CharacterAsset? _script;

        public override object Target => _script!;

        private int _selected;
        private int _helperId;
        public override void OpenEditor(ImGuiRenderer imGuiRenderer, RenderContext renderContext, object target)
        {
            _script = (CharacterAsset)target;

            if (!Stages.ContainsKey(_script.Guid))
            {
                Stage stage = new(imGuiRenderer, renderContext);

                Stages[_script.Guid] = stage;

                // Add entities here?
                //int entityId = stage.AddEntityWithoutAsset(
                //    _particleAsset.GetTrackerComponent(), new PositionComponent(0, 0));
                
                // Activate dialog system here:
                stage.ActivateSystemsWith(enable: true, typeof(DialogueEditorAttribute));
                stage.DeactivateSystem(typeof(EntitiesSelectorSystem));
                _helperId = stage.AddEntityWithoutAsset();
            }
        }

        public override void DrawEditor()
        {
            GameLogger.Verify(Stages is not null);
            GameLogger.Verify(_script is not null);

            if (!Stages.TryGetValue(_script.Guid, out var stageInfo))
            {
                GameLogger.Warning("Unitialized stage for particle editor?");
                return;
            }

            Stage stage = stageInfo;

            if (ImGui.BeginTable("script_table", 2, ImGuiTableFlags.Resizable))
            {
                ImGui.TableSetupColumn("a", ImGuiTableColumnFlags.WidthStretch, -1f, 1);
                ImGui.TableSetupColumn("b", ImGuiTableColumnFlags.WidthFixed, 600, 0);

                ImGui.TableNextRow();
                ImGui.TableNextColumn();

                if (Stages.ContainsKey(_script.Guid))
                {
                    stage.EditorHook.DrawSelection = false;
                    stage.Draw();
                }

                ImGui.TableNextColumn();

                float height = ImGui.GetWindowContentRegionMax().Y - 60;
                ImGui.BeginChild("situations_table", new System.Numerics.Vector2(-1, height));

                for (int i = 0; i < _script.Situations.Length; i++)
                {
                    var situation = _script.Situations[i];
                    if (ImGui.Selectable($"{'\uf0c2'}{situation.Name}"))
                    {
                        _selected = i;
                        ImGui.TreePop();
                        stage.AddOrReplaceComponentOnEntity(_helperId, new DialogueNodeEditorComponent(situation));
                    }
                }


                ImGui.EndChild();
                ImGui.EndTable();
            }
        }

        private bool TreeEntityGroupNode(string name, System.Numerics.Vector4 textColor, char icon = '\ue1b0', ImGuiTreeNodeFlags flags = ImGuiTreeNodeFlags.None) =>
            ImGuiHelpers.TreeNodeWithIconAndColor(
                icon: icon,
                label: name,
                flags: ImGuiTreeNodeFlags.Framed | ImGuiTreeNodeFlags.FramePadding | flags,
                text: textColor,
                background: Game.Profile.Theme.BgFaded,
                active: Game.Profile.Theme.Bg);
    }
}
