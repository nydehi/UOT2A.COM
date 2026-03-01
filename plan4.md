Wait! In `Target.cs`, it already checks `Deleted` and `CanSee`.
However, `CanScissor` is explicitly called from `InternalTarget.OnTarget`:
```csharp
					if ( CanScissor( from, obj ) && obj.Scissor( from, m_Item ) )
```
Since it's called there, moving the code into `CanScissor` will apply it everywhere.

Let's check if there are any exceptions. Can you cut something that is NOT in your backpack?
Like, can you cut wool off a sheep with scissors? No, that's done with blades/knives, not scissors.
Is there any `IScissorable` that isn't an `Item`?
`BonePile` and `RibCage` are items.
What about `DeathRobe.cs`?
```csharp
        public new bool Scissor( Mobile from, Scissors scissors )
        {
            from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
            return false;
        }
```
If we move the check to `CanScissor`, we'll see:
```csharp
            if ( !item.IsChildOf( from.Backpack ) )
            {
                from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
                return false;
            }
```
If the death robe is worn, we get "must be in backpack" instead of "can not be used on that to produce anything." That seems totally fine.
Wait, if you take off the death robe and put it in your backpack, it's `Nontransferable`. Wait, is it?
Let's see: `DeathRobe` inherits `Robe`. Is it `Nontransferable`? No, it's just `LootType.Newbied`. It doesn't set `QuestItem = true`.
So in the backpack, it will say "can not be used to produce anything" which is exactly what the override says. But wait! If we do this, the backpack check will trigger first if it's on the corpse or equipped. That's a better message anyway.

Is there any code in `Scissors.cs` that prevents it? No.
Let's review the planned changes to `Scissors.cs`:

```csharp
<<<<<<< SEARCH
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
=======
		public static bool CanScissor( Mobile from, IScissorable obj )
		{
			if ( obj is Item )
			{
				Item item = (Item)obj;

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

			return true;
		}
>>>>>>> REPLACE
```

Now let's review the removals:

In `Scripts/Items/Armor/BaseArmor.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
				return false;
			}

			CraftSystem system = DefTailoring.CraftSystem;
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			CraftSystem system = DefTailoring.CraftSystem;
>>>>>>> REPLACE
```

In `Scripts/Items/Clothing/BaseClothing.cs`:
```csharp
<<<<<<< SEARCH
		public virtual bool Scissor( Mobile from, Scissors scissors )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
				return false;
			}

			CraftSystem system = DefTailoring.CraftSystem;
=======
		public virtual bool Scissor( Mobile from, Scissors scissors )
		{
			CraftSystem system = DefTailoring.CraftSystem;
>>>>>>> REPLACE
```

In `Scripts/Items/Resources/Tailor/Cloth.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
>>>>>>> REPLACE
```

In `Scripts/Items/Resources/Tailor/Hides.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			base.ScissorHelper( from, new Leather(), 1 );

			return true;
		}
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			base.ScissorHelper( from, new Leather(), 1 );

			return true;
		}
>>>>>>> REPLACE
```

In `Scripts/Items/Resources/Tailor/BoltOfCloth.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			base.ScissorHelper( from, new Cloth(), 50 );

			return true;
		}
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			base.ScissorHelper( from, new Cloth(), 50 );

			return true;
		}
>>>>>>> REPLACE
```

In `Scripts/Items/Resources/Tailor/UncutCloth.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
>>>>>>> REPLACE
```

In `Scripts/Items/Misc/OilCloth.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) )
				return false;

			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
>>>>>>> REPLACE
```

In `Scripts/Items/Body Parts/BonePile.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) )
				return false;

			base.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 10, 15 ) );

			return true;
		}
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			base.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 10, 15 ) );

			return true;
		}
>>>>>>> REPLACE
```

In `Scripts/Items/Body Parts/RibCage.cs`:
```csharp
<<<<<<< SEARCH
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) )
				return false;

			base.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 3, 5 ) );

			return true;
		}
=======
		public bool Scissor( Mobile from, Scissors scissors )
		{
			base.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 3, 5 ) );

			return true;
		}
>>>>>>> REPLACE
```
