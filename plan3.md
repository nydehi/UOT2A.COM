Let's see: `BoltOfCloth.cs`, `Cloth.cs`, `Hides.cs`, `UncutCloth.cs`, `OilCloth.cs`, `BonePile.cs`, `RibCage.cs`. They ALL have:
`if ( Deleted || !from.CanSee( this ) ) return false;`

And `BaseArmor.cs`, `BaseClothing.cs` have:
```csharp
if ( !IsChildOf( from.Backpack ) )
{
    from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
    return false;
}
```

Could the TODO mean moving `Deleted`, `CanSee`, and `!IsChildOf( from.Backpack )`? Wait, if we enforce `!IsChildOf( from.Backpack )` on `BoltOfCloth`, is that correct? In UO, you can definitely cut a bolt of cloth if it's on the ground or in a chest! Or maybe you can't?
Actually, `502437` literally translates to "Items you wish to cut must be in your backpack." This suggests that scissors might require the target item to be in the backpack. Let's see if other tools require backpack.
Let's check `CanScissor` in standard emulators. In ServUO, `CanScissor` checks:
```csharp
if ( obj is Item item )
{
    if ( item.Deleted || !from.CanSee( item ) )
        return false;

    if ( !item.IsChildOf( from.Backpack ) )
    {
        from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
        return false;
    }

    if ( item.Nontransferable )
    {
        from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
        return false;
    }
}
```
Wait, if I do this, `BoltOfCloth` on the ground won't be scissorable. Is that OSI accurate? Yes! In OSI, almost everything you use tools on must be in your backpack.

Let's check if the TODO meant moving `Deleted || !from.CanSee( this )` as well as `!IsChildOf(from.Backpack)`?
If I move `Deleted || !from.CanSee( this )` AND `!IsChildOf( from.Backpack )` to `Scissors.CanScissor`, I can remove those checks from ALL implementation classes!

Let's check `RunUO` source on Github or similar emulator (ServUO):
In ServUO `Scissors.cs`:
```csharp
        public static bool CanScissor(Mobile from, IScissorable obj)
        {
            if (obj is Item item)
            {
                if (item.Deleted || !from.CanSee(item))
                    return false;

                if (!item.IsChildOf(from.Backpack))
                {
                    from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack.
                    return false;
                }

                if (item.Nontransferable)
                {
                    from.SendLocalizedMessage(502440); // Scissors can not be used on that to produce anything.
                    return false;
                }
            }

            return true;
        }
```
Yes, this is EXACTLY what it's supposed to be!
And then I would remove the checks from:
- BaseArmor.cs
- BaseClothing.cs
- Cloth.cs
- Hides.cs
- BoltOfCloth.cs
- UncutCloth.cs
- OilCloth.cs
- BonePile.cs
- RibCage.cs
