1. **Understand the Goal**: The `Scissors.cs` file has a `TODO` on line 85 in the `CanScissor` method:
   `// TODO: Move other general checks from the different implementations here`
   Looking at other implementations of `IScissorable.Scissor` across the codebase, there are common checks performed *before* attempting the cutting action:
   - `if ( Deleted || !from.CanSee( this ) ) return false;` (In Cloth, Hides, BoltOfCloth, UncutCloth, OilCloth, BonePile, RibCage, Log (not Scissorable but similar))
   - `if ( !IsChildOf( from.Backpack ) ) { from.SendLocalizedMessage( 502437 ); /* Items you wish to cut must be in your backpack. */ return false; }` (In BaseArmor, BaseClothing)

2. **Assess the Impact**:
   - If we move `Deleted || !from.CanSee( obj )` into `Scissors.CanScissor( Mobile from, IScissorable obj )`, we can remove it from individual `Scissor` implementations, reducing code duplication.
   - For `!IsChildOf( from.Backpack )`, note that it is only checked in `BaseArmor` and `BaseClothing`. In contrast, `Cloth`, `Hides`, `BoltOfCloth`, `UncutCloth`, `OilCloth`, `BonePile`, `RibCage` do *not* have this backpack requirement. Wait, maybe they *should* have it? Let me check if OSI required cloth to be in backpack to cut it. Actually, wait, `ScissorHelper` in `Server.Item` checks:
     `if ( !(thisParent is Container) || !((Container)thisParent).TryDropItem( from, newItem, false ) ) newItem.MoveToWorld( worldLoc, thisMap );`
     It doesn't strictly require the item to be in the backpack. Let's see if we should enforce backpack restriction globally or just keep it in Armor/Clothing.
     Wait, the `TODO` says "other general checks". `Deleted || !from.CanSee` is definitely a general check, but `obj` is `IScissorable`, not necessarily an `Item`! However, all `IScissorable` implementations in RunUO are `Item` subclasses.
     Wait, in `Scissors.cs`:
     ```csharp
     public static bool CanScissor( Mobile from, IScissorable obj )
     {
         if ( obj is Item && ( (Item)obj ).Nontransferable )
         ...
     ```
     We can add:
     ```csharp
         Item item = obj as Item;

         if ( item != null )
         {
             if ( item.Deleted || !from.CanSee( item ) )
                 return false;

             if ( item.Nontransferable )
             {
                 from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
                 return false;
             }
         }
     ```
     Wait, what about the backpack check?
     Let's check `CanScissor` in `Scissors.cs`:
     Does it check backpack? No.

Let's double check if we can safely move `!from.CanSee( item )` and `item.Deleted`.
Yes, this is completely safe and logically general.

Wait, do any `Scissor` implementations do other general checks?
`DeathRobe`: `from.SendLocalizedMessage( 502440 ); return false;`
`BaseShoes`:
```csharp
        public override bool Scissor( Mobile from, Scissors scissors )
        {
            if( DefaultResource == CraftResource.None )
                return base.Scissor( from, scissors );

            from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
            return false;
        }
```

If we just move the `Deleted` and `CanSee` checks to `CanScissor`, we can remove them from all the individual implementations.
What about `!IsChildOf( from.Backpack )`? Should that be a general check?
Wait, you can cut things on the ground if they aren't armor/clothing? Let's look at `Scissors.cs` `OnTarget`. It doesn't enforce backpack, except that range checks are handled by `InternalTarget` (range 2 by default, since base( 2, false, TargetFlags.None )).
If we enforce backpack for *everything*, we might change mechanics. Let's stick to moving `Deleted || !from.CanSee(this)` which is currently in almost every other class except `BaseArmor` and `BaseClothing`! Wait, `BaseArmor` and `BaseClothing` *don't* have `Deleted || !from.CanSee(this)`. Why?
Because `Target` system already checks if the targeted object is deleted or can't be seen?
Let's look at `Target.cs` or `InternalTarget` in `Scissors.cs`:
```csharp
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted )
					return;
...
```
Does `Target.cs` check `Deleted` or `CanSee` for the targeted object?
