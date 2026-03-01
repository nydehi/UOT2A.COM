using System.IO;

public class Test {
	public void Write( long value )
	{
		m_Buffer[0] = (byte)(value >> 56);
		m_Buffer[1] = (byte)(value >> 48);
		m_Buffer[2] = (byte)(value >> 40);
		m_Buffer[3] = (byte)(value >> 32);
		m_Buffer[4] = (byte)(value >> 24);
		m_Buffer[5] = (byte)(value >> 16);
		m_Buffer[6] = (byte)(value >>  8);
		m_Buffer[7] = (byte) value;

		m_Stream.Write( m_Buffer, 0, 8 );
	}
}
