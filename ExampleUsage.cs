using UnityEngine;
using ImGuiNET;

public class DearImGuiDemo : MonoBehaviour
{
    // Some references to private controllers that manage health, movement, enemies etc.
    public EnemySpawnCTRL enemySpawnCTRL;
    public HealthCTRL healthCTRL;
    public MovementCTRL movementCTRL;
    public SpellDiceCTRL spellDiceCTRL;
    public SpellLists spellLists;

    // Subscribe to Layout events
    void OnEnable() {
        ImGuiUn.Layout += OnLayout;
    }
    // Unsubscribe as well
    void OnDisable() {
        ImGuiUn.Layout -= OnLayout;
    }

    // Some bools for controlling different windows
    private bool p_open_enemySpawner = false;
    private bool p_open_playerOptions = false;
    private bool p_open_spells = false;

    // Controll everything from the function that subscribes to Layout events
    void OnLayout() {

        ShowMainHeaderBar();

        // The IF checks is what controls whether the window is actually displayed
        if (p_open_enemySpawner) {
            ShowEnemySpawnMenu();
        }
        if (p_open_playerOptions) {
            ShowPlayerOptions();
        }
        if (p_open_spells) {
            ShowSpellOptions();
        }
    }

    // Top bar creation
    private void ShowMainHeaderBar() {
        ImGui.BeginMainMenuBar();
        // Things in Imgui are inbetween begin and end contexts
        // Begins also usually return boolean, if that bool is true then it means that
        //      that particular context is currently opened. Here we only display menu
        //      items if menu Cheats is opened.
        if (ImGui.BeginMenu("Cheats")) {
            ImGui.MenuItem("Enemy Spawner", null, ref p_open_enemySpawner);
            ImGui.Separator();  // Just a horizontal line in the dropdown
            ImGui.MenuItem("Player Options", null, ref p_open_playerOptions);
            ImGui.MenuItem("Spell Options", null, ref p_open_spells);
            ImGui.EndMenu();    // Make sure to end the contexts
        }
        ImGui.EndMainMenuBar();
    }

    private void ShowEnemySpawnMenu() {
        // We end early if we shouldn't actually draw the window
        if (!ImGui.Begin("Enemy Spawner", ref p_open_enemySpawner)) {
            ImGui.End();
            return;
        }

        // Just a button with an action inside if statement
        // Button size width of -1 fills the window horizontally
        if (ImGui.Button("Destroy all enemies", new Vector2(-1, 20))) { foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")) { Destroy(enemy); } }

        if (ImGui.CollapsingHeader("Rats")) {
            if (ImGui.Button("Rusty Rat", new Vector2(-1, 20)))    { enemySpawnCTRL.SpawnEnemyAtPosition(1, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Basic Rat", new Vector2(-1, 20)))    { enemySpawnCTRL.SpawnEnemyAtPosition(0, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Trained Rat", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(2, new Vector3(0.0f, 1.0f, 0.0f)); }
        }
        if (ImGui.CollapsingHeader("Skeletons")) {
            if (ImGui.Button("Rusty Skeleton", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(3, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Basic Skeleton", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(4, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Trained Skeleton", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(5, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Hardened Skeleton", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(6, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Elite Skeleton", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(7, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Demonic Skeleton", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(8, new Vector3(0.0f, 1.0f, 0.0f)); }
            if (ImGui.Button("Overlord Skeleton", new Vector2(-1, 20))) { enemySpawnCTRL.SpawnEnemyAtPosition(9, new Vector3(0.0f, 1.0f, 0.0f)); }
        }
        ImGui.End();
    }

    private void ShowPlayerOptions() {
        if (!ImGui.Begin("Player Options", ref p_open_playerOptions)) {
            ImGui.End();
            return;
        }

        int maxHealth = healthCTRL.maxHealth;
        if(ImGui.InputInt("Max Health", ref maxHealth)){
            healthCTRL.maxHealth = maxHealth;
        }

        int currentHealth = healthCTRL.GetHealth();
        if(ImGui.SliderInt("Current Health", ref currentHealth, 0, healthCTRL.maxHealth)) {
            healthCTRL.SetHealth(currentHealth);
        }

        int healthRegen = healthCTRL.HEALTH_REGEN;
        if (ImGui.InputInt("Health Regen", ref healthRegen)) {
            healthCTRL.HEALTH_REGEN = healthRegen;
        }


        float accel = movementCTRL.accelerationMult;
        if (ImGui.InputFloat("Acceleration", ref accel)) {
            movementCTRL.accelerationMult = accel;
        }

        ImGui.End();
    }

    private void ShowSpellOptions() {
        if (!ImGui.Begin("Spell Options", ref p_open_spells)) {
            ImGui.End();
            return;
        }

        for (int diceIndex = 0; diceIndex < spellLists.dice.Count; diceIndex++) {
            ImGui.PushID(diceIndex.ToString());
            ImGui.Text(spellLists.dice[diceIndex].diceName); ImGui.SameLine();
            if (ImGui.Button("1", new Vector2(30, 20))) { spellDiceCTRL.SlotNewDice(0, spellLists.dice[diceIndex]); } ImGui.SameLine();
            if (ImGui.Button("2", new Vector2(30, 20))) { spellDiceCTRL.SlotNewDice(1, spellLists.dice[diceIndex]); } ImGui.SameLine();
            if (ImGui.Button("3", new Vector2(30, 20))) { spellDiceCTRL.SlotNewDice(2, spellLists.dice[diceIndex]); } ImGui.SameLine();
            if (ImGui.Button("4", new Vector2(30, 20))) { spellDiceCTRL.SlotNewDice(3, spellLists.dice[diceIndex]); } ImGui.SameLine();
            if (ImGui.Button("Q", new Vector2(30, 20))) { spellDiceCTRL.SlotNewDice(4, spellLists.dice[diceIndex]); } ImGui.SameLine();
            if (ImGui.Button("E", new Vector2(30, 20))) { spellDiceCTRL.SlotNewDice(5, spellLists.dice[diceIndex]); } ImGui.SameLine();
            if (ImGui.Button("F", new Vector2(30, 20))) { spellDiceCTRL.SlotNewDice(6, spellLists.dice[diceIndex]); }
            ImGui.PopID();
        }

        ImGui.End();
    }
}
