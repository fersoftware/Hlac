using System;
using System.Collections.Generic;
using System.Text;
using Busca;
using Busca.Grafo;
using Busca.Util;

namespace Teste {
	class Program {
		static void Main(string[] args) {
			//-----------------------------------------------------------------
			// EstadoAspiradorDePo
			//-----------------------------------------------------------------
/*
			EstadoAspiradorDePo inicial = new EstadoAspiradorDePo(EstadoAspiradorDePo.SUJO, EstadoAspiradorDePo.SUJO, EstadoAspiradorDePo.DIREITA, "Inicial");
			No resultado;

			Console.WriteLine(inicial.Descricao);

			Console.WriteLine();
			BuscaLargura buscaLargura = new BuscaLargura();
			resultado = buscaLargura.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em largura");
			} else {
				Console.WriteLine("Busca em largura (" + buscaLargura.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidade buscaProfundidade = new BuscaProfundidade();
			resultado = buscaProfundidade.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade");
			} else {
				Console.WriteLine("Busca em profundidade (" + buscaProfundidade.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeRecursiva buscaProfundidadeRecursiva = new BuscaProfundidadeRecursiva();
			resultado = buscaProfundidadeRecursiva.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade recursiva");
			} else {
				Console.WriteLine("Busca em profundidade recursiva (" + buscaProfundidadeRecursiva.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeIterativa buscaProfundidadeIterativa = new BuscaProfundidadeIterativa();
			resultado = buscaProfundidadeIterativa.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade iterativa");
			} else {
				Console.WriteLine("Busca em profundidade iterativa (" + buscaProfundidadeIterativa.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}
            */
			//-----------------------------------------------------------------
			// EstadoAspiradorDePoGrande
			//-----------------------------------------------------------------

			int[] quartosIniciais = new int[EstadoAspiradorDePoGrande.TAMANHO];
			for (int i = 0; i < EstadoAspiradorDePoGrande.TAMANHO; i++) {
				quartosIniciais[i] = EstadoAspiradorDePoGrande.SUJO;
			}

			EstadoAspiradorDePoGrande inicial = new EstadoAspiradorDePoGrande(quartosIniciais, EstadoAspiradorDePoGrande.TAMANHO / 2, "Inicial");
			No resultado;

			Console.WriteLine(inicial.Descricao);

			Console.WriteLine();
			BuscaHeuristicaAEstrela buscaHeuristicaAEstrela = new BuscaHeuristicaAEstrela();
			resultado = buscaHeuristicaAEstrela.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica A*");
			} else {
				Console.WriteLine("Busca heuristica A* (" + buscaHeuristicaAEstrela.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaHeuristicaSubidaDaMontanha buscaHeuristicaSubidaDaMontanha = new BuscaHeuristicaSubidaDaMontanha();
			resultado = buscaHeuristicaSubidaDaMontanha.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica subida da montanha");
			} else {
				Console.WriteLine("Busca heuristica subida da montanha (" + buscaHeuristicaSubidaDaMontanha.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaLargura buscaLargura = new BuscaLargura();
			resultado = buscaLargura.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em largura");
			} else {
				Console.WriteLine("Busca em largura (" + buscaLargura.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidade buscaProfundidade = new BuscaProfundidade();
			resultado = buscaProfundidade.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade");
			} else {
				Console.WriteLine("Busca em profundidade (" + buscaProfundidade.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeRecursiva buscaProfundidadeRecursiva = new BuscaProfundidadeRecursiva();
			resultado = buscaProfundidadeRecursiva.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade recursiva");
			} else {
				Console.WriteLine("Busca em profundidade recursiva (" + buscaProfundidadeRecursiva.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeIterativa buscaProfundidadeIterativa = new BuscaProfundidadeIterativa();
			resultado = buscaProfundidadeIterativa.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade iterativa");
			} else {
				Console.WriteLine("Busca em profundidade iterativa (" + buscaProfundidadeIterativa.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			//-----------------------------------------------------------------
			// EstadoHLAC
			//-----------------------------------------------------------------

			/*EstadoHLAC inicial = new EstadoHLAC(EstadoHLAC.MARGEM_INICIAL, EstadoHLAC.MARGEM_INICIAL, EstadoHLAC.MARGEM_INICIAL, EstadoHLAC.MARGEM_INICIAL, "Inicial");
			No resultado;

			Console.WriteLine(inicial.Descricao);

			Console.WriteLine();
			BuscaHeuristicaAEstrela buscaHeuristicaAEstrela = new BuscaHeuristicaAEstrela();
			resultado = buscaHeuristicaAEstrela.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica A*");
			} else {
				Console.WriteLine("Busca heuristica A* (" + buscaHeuristicaAEstrela.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaHeuristicaSubidaDaMontanha buscaHeuristicaSubidaDaMontanha = new BuscaHeuristicaSubidaDaMontanha();
			resultado = buscaHeuristicaSubidaDaMontanha.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica subida da montanha");
			} else {
				Console.WriteLine("Busca heuristica subida da montanha (" + buscaHeuristicaSubidaDaMontanha.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaLargura buscaLargura = new BuscaLargura();
			resultado = buscaLargura.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em largura");
			} else {
				Console.WriteLine("Busca em largura (" + buscaLargura.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidade buscaProfundidade = new BuscaProfundidade();
			resultado = buscaProfundidade.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade");
			} else {
				Console.WriteLine("Busca em profundidade (" + buscaProfundidade.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeRecursiva buscaProfundidadeRecursiva = new BuscaProfundidadeRecursiva();
			resultado = buscaProfundidadeRecursiva.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade recursiva");
			} else {
				Console.WriteLine("Busca em profundidade recursiva (" + buscaProfundidadeRecursiva.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeIterativa buscaProfundidadeIterativa = new BuscaProfundidadeIterativa();
			resultado = buscaProfundidadeIterativa.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade iterativa");
			} else {
				Console.WriteLine("Busca em profundidade iterativa (" + buscaProfundidadeIterativa.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}*/

			//-----------------------------------------------------------------
			// EstadoMissionarioCanibal
			//-----------------------------------------------------------------

			/*EstadoMissionarioCanibal inicial = new EstadoMissionarioCanibal(3, 3, EstadoMissionarioCanibal.MARGEM_INICIAL, "Inicial");
			No resultado;

			Console.WriteLine(inicial.Descricao);

			Console.WriteLine();
			BuscaHeuristicaAEstrela buscaHeuristicaAEstrela = new BuscaHeuristicaAEstrela();
			resultado = buscaHeuristicaAEstrela.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica A*");
			} else {
				Console.WriteLine("Busca heuristica A* (" + buscaHeuristicaAEstrela.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaHeuristicaSubidaDaMontanha buscaHeuristicaSubidaDaMontanha = new BuscaHeuristicaSubidaDaMontanha();
			resultado = buscaHeuristicaSubidaDaMontanha.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica subida da montanha");
			} else {
				Console.WriteLine("Busca heuristica subida da montanha (" + buscaHeuristicaSubidaDaMontanha.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaLargura buscaLargura = new BuscaLargura();
			resultado = buscaLargura.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em largura");
			} else {
				Console.WriteLine("Busca em largura (" + buscaLargura.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidade buscaProfundidade = new BuscaProfundidade();
			resultado = buscaProfundidade.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade");
			} else {
				Console.WriteLine("Busca em profundidade (" + buscaProfundidade.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeRecursiva buscaProfundidadeRecursiva = new BuscaProfundidadeRecursiva();
			resultado = buscaProfundidadeRecursiva.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade recursiva");
			} else {
				Console.WriteLine("Busca em profundidade recursiva (" + buscaProfundidadeRecursiva.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeIterativa buscaProfundidadeIterativa = new BuscaProfundidadeIterativa();
			resultado = buscaProfundidadeIterativa.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade iterativa");
			} else {
				Console.WriteLine("Busca em profundidade iterativa (" + buscaProfundidadeIterativa.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}*/

			//-----------------------------------------------------------------
			// EstadoRainha
			//-----------------------------------------------------------------

			/*EstadoRainha inicial = new EstadoRainha("Inicial");
			No resultado;

			Console.WriteLine(inicial.Descricao);

			Console.WriteLine();
			BuscaHeuristicaAEstrela buscaHeuristicaAEstrela = new BuscaHeuristicaAEstrela();
			resultado = buscaHeuristicaAEstrela.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica A*");
			} else {
				Console.WriteLine("Busca heuristica A* (" + buscaHeuristicaAEstrela.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaHeuristicaSubidaDaMontanha buscaHeuristicaSubidaDaMontanha = new BuscaHeuristicaSubidaDaMontanha();
			resultado = buscaHeuristicaSubidaDaMontanha.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica subida da montanha");
			} else {
				Console.WriteLine("Busca heuristica subida da montanha (" + buscaHeuristicaSubidaDaMontanha.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			// TODAS AS BUSCAS SEM HEURÍSTICA OU NÃO RESOLVEM, OU DEMORAM
			// *MUITO* PARA RESOLVER!

			//Console.WriteLine();
			//BuscaLargura buscaLargura = new BuscaLargura();
			//resultado = buscaLargura.Buscar(inicial);
			//if (resultado == null) {
			//	Console.WriteLine("Sem solucao para busca em largura");
			//} else {
			//	Console.WriteLine("Busca em largura (" + buscaLargura.Status.NosVisitados + "): " + resultado.MontarCaminho());
			//}

			//Console.WriteLine();
			//BuscaProfundidade buscaProfundidade = new BuscaProfundidade();
			//resultado = buscaProfundidade.Buscar(inicial);
			//if (resultado == null) {
			//	Console.WriteLine("Sem solucao para busca em profundidade");
			//} else {
			//	Console.WriteLine("Busca em profundidade (" + buscaProfundidade.Status.NosVisitados + "): " + resultado.MontarCaminho());
			//}

			//Console.WriteLine();
			//BuscaProfundidadeRecursiva buscaProfundidadeRecursiva = new BuscaProfundidadeRecursiva();
			//resultado = buscaProfundidadeRecursiva.Buscar(inicial);
			//if (resultado == null) {
			//	Console.WriteLine("Sem solucao para busca em profundidade recursiva");
			//} else {
			//	Console.WriteLine("Busca em profundidade recursiva (" + buscaProfundidadeRecursiva.Status.NosVisitados + "): " + resultado.MontarCaminho());
			//}

			//Console.WriteLine();
			//BuscaProfundidadeIterativa buscaProfundidadeIterativa = new BuscaProfundidadeIterativa();
			//resultado = buscaProfundidadeIterativa.Buscar(inicial);
			//if (resultado == null) {
			//	Console.WriteLine("Sem solucao para busca em profundidade iterativa");
			//} else {
			//	Console.WriteLine("Busca em profundidade iterativa (" + buscaProfundidadeIterativa.Status.NosVisitados + "): " + resultado.MontarCaminho());
			//}*/

			//-----------------------------------------------------------------
			// EstadoMapa
			//-----------------------------------------------------------------

			/*// vamos traçar uma rota da cidade 8 para a cidade 15 (se for possível)
			EstadoMapa.CidadeFinal = EstadoMapa.Mapa.GetVertice(15);
			EstadoMapa inicial = new EstadoMapa(EstadoMapa.Mapa.GetVertice(8), 0);
			No resultado;

			Console.WriteLine(inicial.Descricao);

			Console.WriteLine();
			BuscaHeuristicaAEstrela buscaHeuristicaAEstrela = new BuscaHeuristicaAEstrela();
			resultado = buscaHeuristicaAEstrela.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica A*");
			} else {
				Console.WriteLine("Busca heuristica A* (" + buscaHeuristicaAEstrela.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaHeuristicaSubidaDaMontanha buscaHeuristicaSubidaDaMontanha = new BuscaHeuristicaSubidaDaMontanha();
			resultado = buscaHeuristicaSubidaDaMontanha.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca heuristica subida da montanha");
			} else {
				Console.WriteLine("Busca heuristica subida da montanha (" + buscaHeuristicaSubidaDaMontanha.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaLargura buscaLargura = new BuscaLargura();
			resultado = buscaLargura.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em largura");
			} else {
				Console.WriteLine("Busca em largura (" + buscaLargura.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidade buscaProfundidade = new BuscaProfundidade();
			resultado = buscaProfundidade.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade");
			} else {
				Console.WriteLine("Busca em profundidade (" + buscaProfundidade.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeRecursiva buscaProfundidadeRecursiva = new BuscaProfundidadeRecursiva();
			resultado = buscaProfundidadeRecursiva.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade recursiva");
			} else {
				Console.WriteLine("Busca em profundidade recursiva (" + buscaProfundidadeRecursiva.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}

			Console.WriteLine();
			BuscaProfundidadeIterativa buscaProfundidadeIterativa = new BuscaProfundidadeIterativa();
			resultado = buscaProfundidadeIterativa.Buscar(inicial);
			if (resultado == null) {
				Console.WriteLine("Sem solucao para busca em profundidade iterativa");
			} else {
				Console.WriteLine("Busca em profundidade iterativa (" + buscaProfundidadeIterativa.Status.NosVisitados + "): " + resultado.MontarCaminho());
			}*/

			Console.ReadKey();
		}
	}
}
