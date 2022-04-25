using System.ComponentModel;

public enum Round
{
    [Description("Phase 1")]
    Survior = 0,
    [Description("Phase 2")]
    Monster = 1,
    [Description("Phase 3")]
    Resolve = 2,
    [Description("Phase 4")]
    Reset = 3
}
