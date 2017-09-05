using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class No : IComparable<No>, IComparable {
		public readonly Estado Estado;
		public No Pai;
		public int Profundidade;
		// custo real acumulado para gerar esse nó (custo total desde o início)
		public readonly int G;

		public No(Estado estado, No pai) {
			Estado = estado;
			Pai = pai;
			if (pai == null) {
				Profundidade = 0;
				G = estado.Custo;
			} else {
				Profundidade = pai.Profundidade + 1;
				G = estado.Custo + pai.G;
			}
		}

		public override string ToString() {
			return Estado.ToString();
		}

		public override bool Equals(object obj) {
			if (this == obj) {
				return true;
			}
			No n = obj as No;
			if (n == null) {
				return false;
			}
			return n.Estado.Equals(Estado);
		}

		public override int GetHashCode() {
			return Estado.GetHashCode();
		}

		public int F {
			get {
				// estimativa de custo total para chegar à solução final:
				// custo total acumulado até aqui + heurística do nó atual
				return G + Estado.H;
			}
		}

		public int CompareTo(No other) {
			// ordena os nós com base em seus custos totais
			if (G > other.G) {
				return 1; // meu custo é maior (fico depois de other na fila)
			} else if (G < other.G) {
				return -1; // meu custo é menor (fico antes de other na fila)
			}
			return 0; // somos iguais
		}

		public int CompareTo(object obj) {
			if (obj is No) {
				return CompareTo(obj as No);
			}
			return 0; // não tem como comparar
		}

		public string MontarCaminho() {
			return MontarCaminho(this, 0);
		}

		public static string MontarCaminho(No n, int nosAteAqui) {
			if (n == null) {
				return "Nós na solução final: " + nosAteAqui + " ->  ";
			}
			return MontarCaminho(n.Pai, nosAteAqui + 1) + n + "; ";
		}

		public void InvertePaternidade() {
			if (Pai.Pai != null) {
				Pai.InvertePaternidade();
			}
			Pai.Pai = this;
		}

		public void AtualizaProfundidade() {
			// arruma a profundidade de um nó e de seus pais
			if (Pai == null) {
				Profundidade = 0;
			} else {
				Pai.AtualizaProfundidade();
				Profundidade = Pai.Profundidade + 1;
			}
		}

		public bool IsDescendenteNovo(No ancestral) {
			// testa se o nó não tem um ancestral igual a ele
			// (se seu pai, ou o pai do seu pai... é igual a ele)
			if (ancestral == null) {
				return true;
			} else {
				if (ancestral.Estado.Equals(Estado)) {
					return false;
				} else {
					return IsDescendenteNovo(ancestral.Pai);
				}
			}
		}
	}
}
