using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    public class UltrapassaLimiteTabuleiro : Exception
    {
        public UltrapassaLimiteTabuleiro() : base("Ultrapassa limites do tabuleiro.", new IndexOutOfRangeException()) { }

        public UltrapassaLimiteTabuleiro(string message) : base(message) { }

        public UltrapassaLimiteTabuleiro(string message, Exception inner) : base(message, inner) { }

        protected UltrapassaLimiteTabuleiro(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class CorInvalida : Exception
    {
        public CorInvalida() : base("Cor inválida.") { }

        public CorInvalida(string message) : base(message) { }

        public CorInvalida(string message, Exception inner) : base(message, inner) { }

        protected CorInvalida(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
