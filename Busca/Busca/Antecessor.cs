using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public interface Antecessor {
		List<Estado> Antecessores { get; }
	}
}
