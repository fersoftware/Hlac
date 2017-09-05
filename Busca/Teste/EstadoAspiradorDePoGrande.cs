using System;
using System.Collections.Generic;
using System.Text;
using Busca;
using Busca.Grafo;
using Busca.Util;

namespace Teste {
	class EstadoAspiradorDePoGrande : Estado {
		public const int LIMPO = 0;
		public const int SUJO = 1;

		public const int TAMANHO = 16;

		private readonly int[] quartos;
		private readonly int posicao, h;
		private int hashCode;
		private readonly string operacao;

		public EstadoAspiradorDePoGrande(int[] quartos, int posicao, string operacao) {
			this.quartos = quartos;
			this.posicao = posicao;
			this.operacao = operacao;
			h = CalcularH();
			// vamos iniciar o hash code com 0, e calculá-lo apenas quando for necessário
			hashCode = 0;
		}

		public override string ToString() {
			// para poder imprimir a solução completa na tela
			return operacao;
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			EstadoAspiradorDePoGrande e = (obj as EstadoAspiradorDePoGrande);
			if (e == null || e.posicao != posicao) {
				return false;
			}
			for (int i = 0; i < TAMANHO; i++) {
				if (e.quartos[i] != quartos[i]) {
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode() {
			// aqui é necessário que o hash code já tenha sido calculado!!!
			if (hashCode == 0) {
				hashCode = CalcularHashCode();
			}
			return hashCode;
		}

		private int CalcularH() {
			// @@@

			return 0;
		}

		private int CalcularHashCode() {
			int h = posicao;
			for (int i = 0; i < TAMANHO; i++) {
				h = (h * 3) + quartos[i];
			}
			return h;
		}

		public int Custo {
			get {
				// todas as nossas operações têm o mesmo custo: 1
				// (não existe operação mais complexa que outra, ou mais
				// demorada que outra)
				return 1;
			}
		}

		public string Descricao {
			get {
				return "Problema classico do aspirador de po (para varios quartos)";
			}
		}

		public int H {
			get {
				return h;
			}
		}

		public bool IsMeta {
			get {
				// @@@

				return true;
			}
		}

		public IEnumerable<Estado> Sucessores {
			get {
				List<Estado> sucessores = new List<Estado>();

                //Quando começa no indice 0:
                if(posicao == 0)
                {
                    for (int i = posicao; i < TAMANHO; i++)
                    {
                        if(quartos[i] == SUJO)
                        {
                            quartos[i] = LIMPO;

                            if (TAMANHO == i+1)
                            {
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i, "Limpa o quarto"));
                            }
                            else if(TAMANHO < i + 1)
                            {                                
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i + 1, "Limpa o quarto"));
                            }
                        }
                        else 
                        {
                            if (TAMANHO == i + 1)
                            {
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i, "Ir para direita"));
                            }
                            else if (TAMANHO < i + 1)
                            {
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i + 1, "Ir para direita"));
                            }
                        }                     
                    } 
                }
                else if (posicao == TAMANHO - 1)//Quando começa no fim do vetor:
                {
                    for (int i = TAMANHO -1 ; i >= 0; i--)
                    {
                        if (quartos[i] == SUJO)
                        {
                            quartos[i] = LIMPO;

                            if (i == 0)
                            {
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i, "Limpa o quarto"));
                            }
                            else if (i > 0)
                            {
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i - 1, "Limpa o quarto"));
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i, "Ir para direita"));
                            }
                            else if (i > 0)
                            {
                                sucessores.Add(new EstadoAspiradorDePoGrande(quartos, i - 1, "Ir para direita"));
                            }                            
                        }
                    }

                }

                return sucessores;
			}
		}
	}
}
