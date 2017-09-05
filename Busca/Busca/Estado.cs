using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public interface Estado {
		// retorna uma descricao do problema que esta representacao
		// de estado resolve
		string Descricao { get; }

		 // verifica se o estado é considerado como meta (solução final)
		bool IsMeta { get; }

		// custo para geracao apenas deste estado
		// (não é o custo acumulado -> g)
		int Custo { get; }

		// estimativa de custo total para chegar à solução, partindo do estado
		// atual (sua heurística)
		// (caso uma heurística não seja suportada, deve ocorrer uma exceção)
		int H { get; }

		// lista de possíveis estados futuros válidos, partindo deste estado
		IEnumerable<Estado> Sucessores { get; }
	}
}
