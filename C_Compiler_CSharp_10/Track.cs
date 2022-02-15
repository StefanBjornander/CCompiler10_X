using System;
using System.Collections.Generic;

namespace CCompiler {
  public class Track {
    private static int m_count = 0;
    private int m_id;

    public Track(Symbol symbol, Register? register = null) {
      m_id = m_count++;
      Register = register;
      Debug.Assert(symbol != null);
      //Debug.Assert(!symbol.Type.IsStructOrUnion());
      CurrentSize = m_maxSize = symbol.Type.ReturnSize();
    }

    public Track(Type type) {
      m_id = m_count++;
      Debug.Assert(type != null);
      //Debug.Assert(!type.IsStructOrUnion());
      Debug.Assert(!type.IsArrayFunctionOrString());
      CurrentSize = m_maxSize = type.ReturnSize();
    }

    public int CurrentSize { get; set; }

    private int m_maxSize;
    public int MaxSize {
      get { return m_maxSize; }
      set { m_maxSize = Math.Max(m_maxSize, value); }
    }

    private int m_minIndex = -1, m_maxIndex = -1;

    public int Index {
      set {
        m_minIndex = (m_minIndex != -1) ? Math.Min(m_minIndex, value) : value;
        m_maxIndex = Math.Max(m_maxIndex, value);
      }
    }

    public Register? Register {get; set;}
    public bool Pointer {get; set;}

    public static bool Overlaps(Track track1, Track track2) {
      Debug.Assert((track1.m_minIndex != -1) && (track1.m_maxIndex != -1));
      Debug.Assert((track2.m_minIndex != -1) && (track2.m_maxIndex != -1));
      return !(((track1.m_maxIndex < track2.m_minIndex) ||
                (track2.m_maxIndex < track1.m_minIndex)));
    }

    public override string ToString() {
      return m_id.ToString();
    }
  }
}