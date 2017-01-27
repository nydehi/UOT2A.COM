namespace Server.Items
{
    [Furniture]
    [Flipable( 0xB2E, 0xB2F, 0xB31, 0xB30 )]
    public class WoodenThrone : Item
    {
        [Constructable]
        public WoodenThrone() : base(0xB2E)
        {
            Weight = 15.0;
        }

        public WoodenThrone(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if ( Weight == 6.0 )
                Weight = 15.0;
        }
    }
}