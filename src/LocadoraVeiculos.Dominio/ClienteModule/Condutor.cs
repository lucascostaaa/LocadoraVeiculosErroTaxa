using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.ClienteModule
{
    public class Condutor : EntidadeBase<int>, IEquatable<Condutor>
    {
        public Condutor()
        {
        }

        private Condutor(string nome, string endereco, string telefone, string rg, string cpf,
            string cnh, DateTime dataValidadeCnh, int clienteId)
        {
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
            Rg = rg;
            Cpf = cpf;
            Cnh = cnh;
            DataValidadeCnh = dataValidadeCnh;
            ClienteId = clienteId;
        }

        public Condutor(string nome, string endereco, string telefone, string rg, string cpf,
            string numeroCNH, DateTime validadeCNH, Cliente cliente) :
            this(nome, endereco, telefone, rg, cpf, numeroCNH, validadeCNH, cliente.Id)
        {
            Cliente = cliente;
        }


        public string Nome { get; private set; }
        public string Endereco { get; private set; }
        public string Telefone { get; private set; }
        public string Rg { get; private set; }
        public string Cpf { get; private set; }
        public string Cnh { get; private set; }
        public DateTime DataValidadeCnh { get; private set; }
        public int ClienteId { get; private set; }
        public virtual Cliente Cliente { get; set; }


        public override string Validar()
        {
            string resultadoValidacao = "";

            if (string.IsNullOrEmpty(Nome))
                resultadoValidacao = "O atributo nome é obrigatório e não pode ser vazio.";

            if (string.IsNullOrEmpty(Endereco))
                resultadoValidacao = "O atributo endereço é obrigatório e não pode ser vazio.";

            if (Telefone.Length < 9)
                resultadoValidacao = "O atributo Telefone está invalido.";

            if (string.IsNullOrEmpty(Rg))
                resultadoValidacao = "O atributo Numero do Rg é obrigatório e não pode ser vazio.";

            if (string.IsNullOrEmpty(Cpf))
                resultadoValidacao = "O atributo Numero do Cpf é obrigatório e não pode ser vazio.";

            if (string.IsNullOrEmpty(Cnh))
                resultadoValidacao = "O atributo Numero da CNH é obrigatório e não pode ser vazio.";

            if (DataValidadeCnh == DateTime.MinValue)
                resultadoValidacao = "O campo Validade da CNH é obrigatório";

            if (DataValidadeCnh < DateTime.Now)
                resultadoValidacao = "A validade da cnh inserida está expirada, tente novamente";

            if (string.IsNullOrEmpty(Cliente.ToString()))
                resultadoValidacao = "O campo Cliente é obrigatório e não pode ser Vazio";
            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Condutor);
        }

        public bool Equals(Condutor other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Nome.Equals(other.Nome) &&
                   Endereco.Equals(other.Endereco) &&
                   Telefone.Equals(other.Telefone) &&
                   Rg.Equals(other.Rg) &&
                   Cpf.Equals(other.Cpf) &&
                   Cnh.Equals(other.Cnh) &&
                   DataValidadeCnh.Equals(other.DataValidadeCnh) &&
                   ClienteId.Equals(other.ClienteId) &&
                   EqualityComparer<Cliente>.Default.Equals(Cliente, other.Cliente);
        }

        public override int GetHashCode()
        {
            int hashCode = 1641945510;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Endereco);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Telefone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Rg);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cpf);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cnh);
            hashCode = hashCode * -1521134295 + DataValidadeCnh.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Cliente>.Default.GetHashCode(Cliente);
            return hashCode;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}