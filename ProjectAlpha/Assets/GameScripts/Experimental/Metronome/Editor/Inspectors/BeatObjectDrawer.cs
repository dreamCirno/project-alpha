using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace ProjectAlpha
{
    // 创建一个自定义绘制器类，继承 OdinValueDrawer<BeatObject>
    public class BeatObjectDrawer : OdinValueDrawer<BeatObject>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // 获取当前绘制的对象
            BeatObject beatObject = this.ValueEntry.SmartValue;
    
            // 使用一个 Box 围绕所有属性
            SirenixEditorGUI.BeginBox(label);
            SirenixEditorGUI.BeginBoxHeader();
            GUILayout.Label("Beat Object");
            SirenixEditorGUI.EndBoxHeader();
    
            // 绘制 Position 属性
            EditorGUILayout.LabelField("Position");
            beatObject.Position = EditorGUILayout.Vector2Field(GUIContent.none, beatObject.Position);
    
            // 绘制 Start Time 和 End Time 属性
            EditorGUILayout.LabelField("Times");
            SirenixEditorGUI.BeginHorizontalToolbar();
            beatObject.StartTime = EditorGUILayout.IntField("Start Time", beatObject.StartTime);
            beatObject.EndTime = EditorGUILayout.IntField("End Time", beatObject.EndTime);
            SirenixEditorGUI.EndHorizontalToolbar();
    
            // 绘制 IsNewCombo 和 ComboOffset 属性
            EditorGUILayout.LabelField("Combo");
            SirenixEditorGUI.BeginHorizontalToolbar();
            beatObject.IsNewCombo = EditorGUILayout.Toggle("New Combo", beatObject.IsNewCombo);
            beatObject.ComboOffset = EditorGUILayout.IntField("Combo Offset", beatObject.ComboOffset);
            SirenixEditorGUI.EndHorizontalToolbar();
    
            // 结束 Box
            SirenixEditorGUI.EndBox();
    
            // 更新对象
            this.ValueEntry.SmartValue = beatObject;
        }
    }
}