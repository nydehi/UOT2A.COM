Wait, `Target.cs` ALREADY checks `Deleted` and `CanSee` for the targeted object!
So the manual checks inside `Cloth.Scissor`, `Hides.Scissor`, etc., are technically redundant for direct targeting.
BUT what about non-targeted calls to `Scissor()`? The method is public. There could be other callers.
However, the `TODO` in `Scissors.CanScissor` says:
`// TODO: Move other general checks from the different implementations here`

Let's look at `BaseArmor` and `BaseClothing` again:
```csharp
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
				return false;
			}
```

Could the "general check" refer to `!IsChildOf( from.Backpack )`? Wait, is `!IsChildOf( from.Backpack )` required for ALL scissorable items, or only `BaseArmor`/`BaseClothing`?
Let's look at how other shards/emulators do it or look at old OSI behaviour. If you use scissors on a BoltOfCloth on the floor, it probably cuts into cloth on the floor. In fact, `BoltOfCloth` does NOT have an `IsChildOf(from.Backpack)` check!

What else is common?
Look at `BaseArmor` and `BaseClothing`:
```csharp
			CraftSystem system = DefTailoring.CraftSystem;
			CraftItem item = system.CraftItems.SearchFor( GetType() );

			if ( item != null && item.Resources.Count == 1 && item.Resources.GetAt( 0 ).Amount >= 2 )
			{
```
This is only for craftable items.

Wait, `BonePile` and `RibCage`:
```csharp
			if ( Deleted || !from.CanSee( this ) )
				return false;

			base.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 10, 15 ) );
```

Wait, `ScissorHelper` uses `Deleted`? Actually, `ScissorHelper` is in `Item.cs`.
If `CanScissor` is the centralized place for checks, maybe we should move `Deleted || !from.CanSee( this )` to `CanScissor` since it's present in 6+ places?
Yes, `Deleted || !from.CanSee(this)` makes the most sense.

Wait! What about the `IsChildOf( from.Backpack )` check? If we check OSI behavior, generally you *must* have the item in your backpack to cut it. Wait, no. You can cut cloth on the ground? Let's assume we just centralize `Deleted` and `CanSee` and `!IsChildOf( from.Backpack )` if it's universal? Let me search for `Items you wish to cut must be in your backpack.`
Only BaseArmor and BaseClothing have it.

Wait, the `TODO` says "Move other general checks from the different implementations here".
If I look at `Scissors.cs`:
```csharp
		public static bool CanScissor( Mobile from, IScissorable obj )
		{
			if ( obj is Item && ( (Item)obj ).Nontransferable )
			{
				from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
				return false;
			}

			// TODO: Move other general checks from the different implementations here

			return true;
		}
```
If we add `Deleted` and `CanSee(obj)` and `!IsChildOf(from.Backpack)` into `CanScissor`?
Wait, if we put `!IsChildOf(from.Backpack)` into `CanScissor`, we'd break cutting un-backpackable things (like `UncutCloth`, wait, can you cut `UncutCloth` on the floor?).
If I look at `RunUO` forums or history, `Items you wish to cut must be in your backpack` is typically applied globally to Scissor operations. Wait, let's see if we should move `IsChildOf` check.
