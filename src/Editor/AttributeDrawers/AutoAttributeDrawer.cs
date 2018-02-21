using Autrage.LEX.NET;
using Autrage.LEX.NET.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AutoAttribute))]
public class AutoAttributeDrawer : PropertyDrawer
{
    public static int NextID => Fields.Max(field => field.ID) + 1;

    private static IEnumerable<IDField> Fields =>
        from mono in Resources.FindObjectsOfTypeAll<MonoBehaviour>()
        from field in mono.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        where field.IsDefined(typeof(AutoAttribute))
        where field.FieldType == typeof(int)
        select new IDField(mono, field);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.PropertyField(position, property, label);
        EditorGUI.EndDisabledGroup();

        if (Application.isPlaying)
        {
            return;
        }

        if (property.propertyType != SerializedPropertyType.Integer)
        {
            Bugger.Warning($"Property must be of type int in order for {nameof(AutoAttributeDrawer)} to work!");
            return;
        }

        if (property.intValue == default(int))
        {
            property.intValue = NextID;
        }
    }

    [MenuItem("IDs/Validate IDs %&v")]
    private static void ValidateIDs()
    {
        IOrderedEnumerable<IDField> fields = Fields.OrderBy(field => field.ID);
        if (fields.None())
        {
            return;
        }

        int nextID = NextID;
        int lastID = fields.First().ID;
        foreach (IDField field in fields.Skip(1))
        {
            if (field.ID <= lastID)
            {
                field.Info.SetValue(field.Mono, nextID++);
            }
        }
    }

    [MenuItem("IDs/Redistribute IDs %&r")]
    private static void RedistributeIDs()
    {
        int nextID = default(int) + 1;
        foreach (IDField field in Fields)
        {
            field.Info.SetValue(field.Mono, nextID++);
        }
    }

    [MenuItem("IDs/Validate IDs %&v", validate = true)]
    [MenuItem("IDs/Redistribute IDs %&r", validate = true)]
    static bool IsNotPlaying() => !Application.isPlaying;

    private struct IDField
    {
        public MonoBehaviour Mono { get; }

        public FieldInfo Info { get; }

        public int ID => (int)Info.GetValue(Mono);

        public IDField(MonoBehaviour mono, FieldInfo field) : this()
        {
            Mono = mono;
            Info = field;
        }
    }
}