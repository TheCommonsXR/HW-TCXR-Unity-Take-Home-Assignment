using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityEditor.SceneManagement;

/// <summary>
/// Custom Editor Window for managing player configurations such as position, health, and damage.
/// </summary>
public class PlayerConfigEditor : EditorWindow
{
    [Serializable]
    public class PlayerConfig
    {
        public Vector3 position;
        public int health;
        public int damage;
        public string savedAt;
    }

    [Serializable]
    public class PlayerConfigCollection
    {
        public List<PlayerConfig> configs = new List<PlayerConfig>();
    }

    private const string FileName = "playerSetting.json";
    private static readonly string FilePath = Path.Combine(Application.dataPath, FileName);

    private Vector3 spawn_position = Vector3.zero;
    private int health = 10;
    private int damage = 1;

    private Vector2 scrollPos;
    private PlayerConfigCollection configCollection = new PlayerConfigCollection();

    [MenuItem("Tools/Game Config Editor")]
    public static void ShowWindow()
    {
        GetWindow<PlayerConfigEditor>("Game Config Editor");
    }

    private void OnEnable()
    {
        LoadConfigs();
    }

    private void OnGUI()
    {
        GUILayout.Label("Current Configuration", EditorStyles.boldLabel);

        spawn_position = EditorGUILayout.Vector3Field("Position", spawn_position);
        health = EditorGUILayout.IntField("Health", health);
        damage = EditorGUILayout.IntField("Damage", damage);

        EditorGUILayout.Space();

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Save and Apply Config", GUILayout.Height(30)))
            {
                AddNewConfig();
                ApplyConfigToScene(new PlayerConfig
                {
                    position = spawn_position,
                    health = health,
                    damage = damage
                });
            }

            if (GUILayout.Button("Reload", GUILayout.Height(30)))
            {
                LoadConfigs();
            }
        }

        EditorGUILayout.Space(10);
        GUILayout.Label("Previous Configurations", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(250));

        if (configCollection.configs.Count == 0)
        {
            EditorGUILayout.HelpBox("No previous configurations found.", MessageType.Info);
        }
        else
        {
            for (int i = configCollection.configs.Count - 1; i >= 0; i--)
            {
                PlayerConfig cfg = configCollection.configs[i];

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Saved At", cfg.savedAt);
                EditorGUILayout.Vector3Field("Position", cfg.position);
                EditorGUILayout.IntField("Health", cfg.health);
                EditorGUILayout.IntField("Damage", cfg.damage);
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Load This Config",GUILayout.Height(30)))
                    {
                        spawn_position = cfg.position;
                        health = cfg.health;
                        damage = cfg.damage;
                        ApplyConfigToScene(cfg);
                    }
                    if (GUILayout.Button("Delete", GUILayout.Height(30)))
                    {
                        configCollection.configs.RemoveAt(i);
                        //Update JSON file after deletion
                        string json = JsonUtility.ToJson(configCollection, true);
                        File.WriteAllText(FilePath, json);
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(4);
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void LoadConfigs()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            if(configCollection != null)
                configCollection.configs.Clear();
            configCollection = JsonUtility.FromJson<PlayerConfigCollection>(json);
        }
        else
        {
            configCollection = new PlayerConfigCollection();
        }
    }

    private void AddNewConfig()
    {
        PlayerConfig newConfig = new PlayerConfig
        {
            position = spawn_position,
            health = health,
            damage = damage,
            savedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        if (CheckForDuplicateConfig(newConfig))
        {
            EditorUtility.DisplayDialog("Duplicate Config", "This configuration already exists. Please modify it before saving.", "OK");
            return;
        }  

        configCollection.configs.Add(newConfig);
        string json = JsonUtility.ToJson(configCollection, true);
        File.WriteAllText(FilePath, json);
    }
    
    private bool CheckForDuplicateConfig(PlayerConfig newConfig)
    {
        foreach (var cfg in configCollection.configs)
        {
            if (cfg.position == newConfig.position && cfg.health == newConfig.health && cfg.damage == newConfig.damage)
            {
                return true; // Duplicate found
            }
        }
        return false; // No duplicate
    }

    private void ApplyConfigToScene(PlayerConfig config)
    {
        // Find player in scene
        var playerController = FindFirstObjectByType<Platformer.Mechanics.PlayerController>();
        if (playerController == null)
        {
            EditorUtility.DisplayDialog("Error", "PlayerController not found in scene.", "OK");
            return;
        }

        // Update player spawn position
        var model = Platformer.Core.Simulation.GetModel<Platformer.Model.PlatformerModel>();
        if (model != null && model.spawnPoint != null)
        {
            model.spawnPoint.transform.position = config.position;
            EditorUtility.SetDirty(model.spawnPoint.transform);
            Debug.Log($"Spawn position updated to: {position}");
        }

        var playerConfigLoader = playerController.GetComponent<PlayerConfigLoader>();
        if (playerConfigLoader != null)
        {
            playerConfigLoader.currentHealth = config.health;
            EditorUtility.SetDirty(playerConfigLoader);
            Debug.Log($"Player current health updated to: {config.health}");
        }

        // Update enemy damage
        playerController.enemyDamage = config.damage;
        EditorUtility.SetDirty(playerController);
        Debug.Log($"Enemy damage updated to: {damage}");

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorUtility.DisplayDialog("Success", "Configuration applied to scene.", "OK");
    }
}