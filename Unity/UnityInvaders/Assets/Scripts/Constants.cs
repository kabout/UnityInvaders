
using System;

public static class Constants
{
    public const int DEFAULT_DEFENSE_ATTACKS_PER_SECOND = 1;
    public const int DEFAULT_DEFENSE_RADIO = 10;
    public const int DEFENSE_MIN_HEALTH = 50;
    public const int DEFENSE_MAX_HEALTH = 150;
    public const int DEFENSE_MIN_RANGE = 5;
    public const int DEFENSE_MAX_RANGE = 20;
    public const int DEFENSE_MIN_DISPERSION = 1;
    public const int DEFENSE_MAX_DISPERSION = 10;
    public const int DEFENSE_MIN_DAMAGE = 5;
    public const int DEFENSE_MAX_DAMAGE = 30;

    public const int MARGIN_MAP = 10;
    public const float MAX_OBSTACLES_PER_AREA_UNIT = 0.0005f;
    public const int MIN_OBSTACLE_RADIUS = 10;
    public const int MAX_OBSTACLE_RADIUS = 25;
    public const float MAX_DEFENSES_PER_AREA_UNIT = 0.0005f;
    public const int ALIEN_PER_SECOND = 2;
    public const float MAX_DEFENSES_DENSITY = 0.6f;
    public const float MAX_OBSTACLES_DENSITY = 0.7f;
    public const float MIN_DEFENSES_DENSITY = 0.15f;
    public const float MIN_OBSTACLES_DENSITY = 0.2f;

    public const int DEFAULT_ALIEN_HEALTH = 150;
    public const int DEFAULT_ALIEN_RANGE = 20;
    public const int DEFAULT_ALIEN_RADIO = 1;
    public const float DEFAULT_ALIEN_DAMAGE = 15;

    public static int GetDefenseMeanCost()
    {
        return (int)Math.Round(((DEFENSE_MIN_HEALTH + DEFENSE_MAX_HEALTH) / 2) * 0.2 +
               ((DEFENSE_MIN_RANGE + DEFENSE_MAX_RANGE) / 2) * 0.3 +
               ((DEFENSE_MIN_DAMAGE + DEFENSE_MAX_DAMAGE) / 2) * 0.4 +
               ((DEFENSE_MIN_DISPERSION + DEFENSE_MAX_DISPERSION) / 2) * 0.1);
    }
}