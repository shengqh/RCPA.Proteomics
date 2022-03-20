/* Copyright (c) 2007
 *   Minghui Jiang, James Anderson, Joel Gillespie, and Martin Mayne.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 1. Redistributions of source code must retain the above copyright notice,
 *      this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *      this list of conditions and the following disclaimer in the
 *      documentation and/or other materials provided with the distribution.
 * 3. The names of its contributors may not be used to endorse or promote
 *      products derived from this software without specific prior written
 *      permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
 * TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

/*
 *	UShuffle.cs
 *	Thu Feb 21 13:58:38 MST 2008
 */

using System;
using System.Collections.Generic;

namespace uShuffle
{
  public class UShuffle
  {
    public static String title = "uShuffle: a useful tool for shuffling biological sequences while preserving the k-let counts";

    public static string Shuffle(string source, int klet)
    {
      UShuffle us = new UShuffle();
      char[] s = source.ToCharArray();
      char[] t = new char[s.Length];

      us.shuffle1(s, s.Length, klet);
      us.shuffle2(t);

      return new string(t);
    }

    public static List<string> Shuffle(string source, int klet, int repeat)
    {
      UShuffle us = new UShuffle();
      char[] s = source.ToCharArray();
      char[] t = new char[s.Length];

      us.shuffle1(s, s.Length, klet);
      List<string> result = new List<string>();

      for (int i = 0; i < repeat; i++)
      {
        us.shuffle2(t);
        result.Add(new string(t));
      }
      return result;
    }

    //public static void Main(string[] argv)
    //{
    //  String str = null;
    //  int n = 1, k = 2;
    //  long seed = DateTime.Now.Ticks;

    //  try
    //  {
    //    for (int i = 0; i < argv.Length; i++)
    //    {
    //      if (argv[i].CompareTo("-s") == 0)
    //      {
    //        if (i + 1 < argv.Length && argv[i + 1][0] != '-')
    //          str = argv[++i];
    //        else
    //          print_help_and_exit();
    //      }
    //      else if (argv[i].CompareTo("-n") == 0)
    //      {
    //        if (i + 1 < argv.Length && argv[i + 1][0] != '-')
    //          n = Int32.Parse(argv[++i]);
    //        else
    //          print_help_and_exit();
    //      }
    //      else if (argv[i].CompareTo("-k") == 0)
    //      {
    //        if (i + 1 < argv.Length && argv[i + 1][0] != '-')
    //          k = Int32.Parse(argv[++i]);
    //        else
    //          print_help_and_exit();
    //      }
    //      else if (argv[i].CompareTo("-seed") == 0)
    //      {
    //        if (i + 1 < argv.Length && argv[i + 1][0] != '-')
    //          seed = Int32.Parse(argv[++i]);
    //        else
    //          print_help_and_exit();
    //      }
    //    }
    //  }
    //  catch (Exception e)
    //  {
    //    print_help_and_exit();
    //  }
    //  if (n <= 0 || str == null)
    //    print_help_and_exit();

    //  UShuffle us = new UShuffle();
    //  us.set_randfunc(new Random((int)seed));
    //  char[] s = str.ToCharArray();
    //  char[] t = new char[s.Length];

    //  us.shuffle1(s, s.Length, k);
    //  for (int i = 0; i < n; i++)
    //  {
    //    us.shuffle2(t);
    //    Console.WriteLine(new String(t));
    //  }
    //}

    private static void print_help_and_exit()
    {
      Console.WriteLine(title + "\nOptions:\n" +
          "  -s <string>     specifies the sequence\n" +
          "  -n <number>     specifies the number of random sequences to generate\n" +
          "  -k <number>     specifies the let size\n" +
          "  -seed <number>  specifies the seed for random number generator\n");
      Environment.Exit(0);
    }

    /* set random function */

    private static Random rand = new Random(DateTime.Now.Millisecond);

    public static void set_randfunc(Random ARand)
    {
      rand = ARand;
    }

    /* global variables for the Eurler algorithm */

    private char[] s_;
    private int l_;
    private int k_;

    private class vertex
    {
      public int[] indices;
      public int n_indices;
      public int i_indices;
      public bool intree;
      public int next;
      public int i_sequence;
    };

    private vertex[] vertices;
    private int n_vertices;
    private int root;

    /* hashtable utility */

    private class hentry
    {
      public hentry next;
      public int i_sequence;
      public int i_vertices;
    };

    private hentry[] entries;
    private hentry[] htable;
    private int htablesize;
    private double hmagic;

    private int hcode(int i_sequence)
    {
      double f = 0.0;

      for (int i = 0; i < k_ - 1; i++)
      {
        f += s_[i_sequence + i];
        f *= hmagic;
      }
      if (f < 0.0)
        f = -f;
      return (int)(htablesize * f) % htablesize;
    }

    private void hinit(int size)
    {
      entries = new hentry[size];
      for (int i = 0; i < size; i++)
        entries[i] = new hentry();
      htable = new hentry[size];
      htablesize = size;
      hmagic = (Math.Sqrt(5.0) - 1.0) / 2.0;
    }

    private void hcleanup()
    {
      entries = null;
      htable = null;
      htablesize = 0;
    }

    private int strncmp(int i, int j, int n)
    {
      for (int k = 0; k < n; k++)
      {
        char ci = s_[i + k];
        char cj = s_[j + k];

        if (ci != cj)
          return ci - cj;
      }
      return 0;
    }

    private void hinsert(int i_sequence)
    {
      int code = hcode(i_sequence);
      hentry e, e2 = entries[i_sequence];

      for (e = htable[code]; e != null; e = e.next)
        if (strncmp(e.i_sequence, i_sequence, k_ - 1) == 0)
        {
          e2.i_sequence = e.i_sequence;
          e2.i_vertices = e.i_vertices;
          return;
        }
      e2.i_sequence = i_sequence;
      e2.i_vertices = n_vertices++;
      e2.next = htable[code];
      htable[code] = e2;
    }

    /* the Euler algorithm */

    public void shuffle1(char[] s, int l, int k)
    {
      int i, n_lets;

      s_ = s;
      l_ = l;
      k_ = k;
      if (k_ >= l_ || k_ <= 1)	/* two special cases */
        return;

      /* use hashtable to find distinct vertices */
      n_lets = l_ - k_ + 2;	/* number of (k-1)-lets */
      n_vertices = 0;
      hinit(n_lets);
      for (i = 0; i < n_lets; i++)
        hinsert(i);
      root = entries[n_lets - 1].i_vertices;	/* the last let */
      vertices = new vertex[n_vertices];
      for (i = 0; i < n_vertices; i++)
        vertices[i] = new vertex();

      /* set i_sequence and n_indices for each vertex */
      for (i = 0; i < n_lets; i++)
      {	/* for each let */
        hentry ev = entries[i];
        vertex v = vertices[ev.i_vertices];

        v.i_sequence = ev.i_sequence;
        if (i < n_lets - 1)	/* not the last let */
          v.n_indices++;
      }

      /* allocate indices for each vertex */
      for (i = 0; i < n_vertices; i++)
      {	/* for each vertex */
        vertex v = vertices[i];

        v.indices = new int[v.n_indices];
      }

      /* populate indices for each vertex */
      for (i = 0; i < n_lets - 1; i++)
      {	/* for each edge */
        hentry eu = entries[i];
        hentry ev = entries[i + 1];
        vertex u = vertices[eu.i_vertices];

        u.indices[u.i_indices++] = ev.i_vertices;
      }
      hcleanup();
    }

    private void permute(char[] t, int n)
    {
      for (int i = n - 1; i > 0; i--)
      {
        int j = rand.Next(i + 1);
        char tmp = t[i]; t[i] = t[j]; t[j] = tmp;	/* swap */
      }
    }

    private void permute(int[] t, int n)
    {
      for (int i = n - 1; i > 0; i--)
      {
        int j = rand.Next(i + 1);
        int tmp = t[i]; t[i] = t[j]; t[j] = tmp;	/* swap */
      }
    }

    private void strncpy(char[] t, char[] s, int n)
    {
      for (int i = 0; i < n; i++)
        t[i] = s[i];
    }

    public void shuffle2(char[] t)
    {
      vertex u, v;
      int i, j;

      /* exact copy case */
      if (k_ >= l_)
      {
        strncpy(t, s_, l_);
        return;
      }

      /* simple permutation case */
      if (k_ <= 1)
      {
        strncpy(t, s_, l_);
        permute(t, l_);
        return;
      }

      /* the Wilson algorithm for random arborescence */
      for (i = 0; i < n_vertices; i++)
        vertices[i].intree = false;
      vertices[root].intree = true;
      for (i = 0; i < n_vertices; i++)
      {
        u = vertices[i];
        while (!u.intree)
        {
          u.next = rand.Next(u.n_indices);
          u = vertices[u.indices[u.next]];
        }
        u = vertices[i];
        while (!u.intree)
        {
          u.intree = true;
          u = vertices[u.indices[u.next]];
        }
      }

      /* shuffle indices to prepare for walk */
      for (i = 0; i < n_vertices; i++)
      {
        u = vertices[i];
        if (i != root)
        {
          j = u.indices[u.n_indices - 1];	/* swap the last one */
          u.indices[u.n_indices - 1] = u.indices[u.next];
          u.indices[u.next] = j;
          permute(u.indices, u.n_indices - 1);	/* permute the rest */
        }
        else
          permute(u.indices, u.n_indices);
        u.i_indices = 0;	/* reset to zero before walk */
      }

      /* walk the graph */
      strncpy(t, s_, k_ - 1);	/* the first let remains the same */
      u = vertices[0];
      i = k_ - 1;
      while (u.i_indices < u.n_indices)
      {
        v = vertices[u.indices[u.i_indices]];
        j = v.i_sequence + k_ - 2;
        t[i++] = s_[j];
        u.i_indices++;
        u = v;
      }
    }

    public void shuffle(char[] s, char[] t, int l, int k)
    {
      shuffle1(s, l, k);
      shuffle2(t);
    }
  }
}
