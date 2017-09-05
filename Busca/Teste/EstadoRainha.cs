using System;
using System.Collections.Generic;
using System.Text;
using Busca;
using Busca.Grafo;
using Busca.Util;

namespace Teste {
	class EstadoRainha : Estado, GeradorDeEstadoAleatorio {
		public const int TAMANHO = 8;

		private readonly Ponto[] rainhas;
		private readonly int h;
		private int hashCode;
		private readonly string operacao;

		public EstadoRainha(string operacao) {
			Random r = new Random();

			// posiciona as rainhas em uma posição inicial aleatória (não podemos
			// deixar que uma rainha fique sobre a outra)
			rainhas = new Ponto[TAMANHO];
			int i = 0;
			while (i < TAMANHO) {
				Ponto novaRainha = new Ponto(r.Next(TAMANHO), r.Next(TAMANHO));

				// verifica se não geramos uma rainha sobre outra já existente
				bool jaExistiaRainha = false;
				for (int a = 0; a < i; a++) {
					if (novaRainha.Equals(rainhas[a])) {
						jaExistiaRainha = true;
						break;
					}
				}

				if (jaExistiaRainha == false) {
					// armazena essa posição e pula para a próxima rainha
					rainhas[i] = novaRainha;
					i++;
				}
			}

			this.operacao = operacao;
			h = CalcularH();
			// vamos iniciar o hash code com 0, e calculá-lo apenas quando for necessário
			hashCode = 0;
		}

		public EstadoRainha(Ponto[] rainhas) {
			this.rainhas = rainhas;
			operacao = null;
			h = CalcularH();
			// vamos iniciar o hash code com 0, e calculá-lo apenas quando for necessário
			hashCode = 0;
		}

		public override string ToString() {
			// para poder imprimir a solução completa na tela
			StringBuilder sb = new StringBuilder();
			if (operacao != null) {
				sb.Append(operacao);
			}
			for (int i = 0; i < TAMANHO; i++) {
				sb.Append(" ");
				sb.Append(rainhas[i]);
			}
			return sb.ToString();
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			EstadoRainha e = (obj as EstadoRainha);
			if (e == null) {
				return false;
			}
			for (int i = 0; i < TAMANHO; i++) {
				if (!e.rainhas[i].Equals(rainhas[i])) {
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
			// nossa heurística será:
			// assumiremos que quanto menos rainhas estiverem se atacando,
			// mais próximo da solução estaremos (lembrando: melhor = menor h)

			int rainhasSeAtacando = 0;

			for (int i = 0; i < TAMANHO; i++) {
				Ponto rainha = rainhas[i];

				// verifica quantas rainhas existem na mesma linha, e
				// quantas existem na mesma coluna da rainha atual
				// além disso, temos que verificar quantas rainhas
				// estão na mesma diagonal que a rainha atual
				for (int a = 0; a < TAMANHO; a++) {
					// temos que desconsiderar a rainha atual :)
					if (a != i) {
						if (rainha.Y == rainhas[a].Y) {
							rainhasSeAtacando++;
						} else if (rainha.X == rainhas[a].X) {
							rainhasSeAtacando++;
						} else {
							// testa a diagonal:
							// se abs(delta X) == abs(delta Y), estão na mesma diagonal
							int deltaX = Math.Abs(rainha.X - rainhas[a].X);
							int deltaY = Math.Abs(rainha.Y - rainhas[a].Y);
							if (deltaX == deltaY) {
								rainhasSeAtacando++;
							}
						}
					}
				}
			}

			return rainhasSeAtacando;
		}

		private int CalcularHashCode() {
			int h = 0;
			for (int i = 0; i < TAMANHO; i++) {
				h = (h * 3) + rainhas[i].GetHashCode();
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
				return
					"Este problema consiste em posicionar 8 " +
					"rainhas do jogo de xadrez em um tabuleiro, " +
					"sendo que nenhuma pode atacar a outra.";
			}
		}

		public int H {
			get {
				return h;
			}
		}

		public bool IsMeta {
			get {
				return (h == 0);
			}
		}

		private EstadoRainha TentarMoverRainha(int rainha, int deltaX, int deltaY) {
			int novoX = rainhas[rainha].X + deltaX;
			int novoY = rainhas[rainha].Y + deltaY;

			if (novoX < 0 || novoX >= TAMANHO || novoY < 0 || novoY >= TAMANHO) {
				// esse movimento tiraria a rainha do tabuleiro
				return null;
			}

			// agora temos que verificar se essa nova rainha está sobre outra
			// rainha já existente (desconsiderando ela própria)
			for (int i = 0; i < TAMANHO; i++) {
				if (i != rainha) {
					if (rainhas[i].X == novoX && rainhas[i].Y == novoY) {
						// esse movimento fez com que a rainha fosse para cima
						// de oura rainha
						return null;
					}
				}
			}

			// tudo certo, movimento válido

			Ponto[] novasRainhas = new Ponto[TAMANHO];
			rainhas.CopyTo(novasRainhas, 0);
			novasRainhas[rainha] = new Ponto(novoX, novoY);

			return new EstadoRainha(novasRainhas);
		}

		public IEnumerable<Estado> Sucessores {
			get {
				List<Estado> sucessores = new List<Estado>();

				// possíveis estados futuros
				// (tenta mover uma rainha por vez para algum dos 8 lados
				// que uma rainha pode se deslocar, desde que a rainha não
				// encoste em outra rainha, e que a rainha não saia do tabuleiro)
				for (int i = 0; i < TAMANHO; i++) {
					EstadoRainha novoEstado;

					novoEstado = TentarMoverRainha(i, 1, 0);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
					novoEstado = TentarMoverRainha(i, -1, 0);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
					novoEstado = TentarMoverRainha(i, 0, 1);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
					novoEstado = TentarMoverRainha(i, 0, -1);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
					novoEstado = TentarMoverRainha(i, 1, 1);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
					novoEstado = TentarMoverRainha(i, -1, 1);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
					novoEstado = TentarMoverRainha(i, 1, -1);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
					novoEstado = TentarMoverRainha(i, -1, -1);
					if (novoEstado != null) {
						sucessores.Add(novoEstado);
					}
				}

				return sucessores;
			}
		}

		public Estado GerarEstadoAleatorio() {
			return new EstadoRainha("RESET!");
		}
	}
}
