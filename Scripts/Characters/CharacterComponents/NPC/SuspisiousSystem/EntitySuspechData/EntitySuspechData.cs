using System;
using Godot;

public partial class EntitySuspechData 
{
    private Entity _entity;
    private int _amountOfSuspision;
    private bool _thinkIsVampire;

    public EntitySuspechData(Entity entity)
    {
        _entity = entity;
        _amountOfSuspision = 0;
        _thinkIsVampire = false;
    }

    public EntitySuspechData(Entity entity, int amountOfSuspision = 0, bool thinkIsVampire = false)
    {
        _entity = entity;
        _amountOfSuspision = amountOfSuspision;
        _thinkIsVampire = thinkIsVampire;
    }

    public Entity Entity { get => _entity; }
    public int AmountOfSuspicion { get => _amountOfSuspision;
        set
        {
            int val = Math.Max(_amountOfSuspision + value, 0);
            _amountOfSuspision = (val > 100) ? 100 : val;
        }
    }
    public bool ThinkIsVampire { get => _thinkIsVampire; set => _thinkIsVampire = value; }
}