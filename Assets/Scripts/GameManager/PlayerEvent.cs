using SecondProject;

public static class SceneEvents
{
    public static event System.Action<PlayerCharacter> OnPlayerSpawned;
    public static event System.Action<PlayerCharacter> OnPlayerDied;

    public static void NotifyPlayerSpawned(PlayerCharacter player)
    {
        OnPlayerSpawned?.Invoke(player);
    }

    public static void NotifyPlayerDied(PlayerCharacter player)
    {
        OnPlayerDied?.Invoke(player);
    }
}
