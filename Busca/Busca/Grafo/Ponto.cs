using System;
using System.Collections.Generic;
using System.Text;

namespace Busca.Grafo {
	public class Ponto {
		public readonly int X, Y;

		public Ponto(int x, int y) {
			X = x;
			Y = y;
		}

		public override string ToString() {
			return "(" + X + ", " + Y + ")";
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			Ponto p = (obj as Ponto);
			return (p != null &&
				p.X == X &&
				p.Y == Y);
		}

		public override int GetHashCode() {
			return (X * 17) + (Y * 79);
		}
	}
}
