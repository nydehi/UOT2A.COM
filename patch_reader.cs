		public long ReadInt64()
		{
			if ( (m_Index + 8) > m_Size )
				return 0;

			return ((long)m_Data[m_Index++] << 56)
				 | ((long)m_Data[m_Index++] << 48)
				 | ((long)m_Data[m_Index++] << 40)
				 | ((long)m_Data[m_Index++] << 32)
				 | ((long)m_Data[m_Index++] << 24)
				 | ((long)m_Data[m_Index++] << 16)
				 | ((long)m_Data[m_Index++] <<  8)
				 |  (long)m_Data[m_Index++];
		}

		public ulong ReadUInt64()
		{
			if ( (m_Index + 8) > m_Size )
				return 0;

			return ((ulong)m_Data[m_Index++] << 56)
				 | ((ulong)m_Data[m_Index++] << 48)
				 | ((ulong)m_Data[m_Index++] << 40)
				 | ((ulong)m_Data[m_Index++] << 32)
				 | ((ulong)m_Data[m_Index++] << 24)
				 | ((ulong)m_Data[m_Index++] << 16)
				 | ((ulong)m_Data[m_Index++] <<  8)
				 |  (ulong)m_Data[m_Index++];
		}
