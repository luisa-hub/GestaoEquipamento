using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GestaoEquipamentos.ConsoleApp.Controladores;
using GestaoEquipamentos.ConsoleApp.Dominio;
namespace GestaoEquipamentos.ConsoleApp.Telas
{
    public class TelaSolicitante : TelaBase
    {
        ControladorSolicitante controladorSolicitante;
       
        public TelaSolicitante(ControladorSolicitante controlador)
           : base("Cadastro de Solicitante")
        {
           
            controladorSolicitante = controlador;
        }
        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo solicitante...");

            bool conseguiuGravar = GravarSolicitante(0);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar inserir o solicitante", TipoMensagem.Erro);
                InserirNovoRegistro();
            }


        }

        public override void EditarRegistro()
        {
            ConfigurarTela("Editando um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do equipamento que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bool conseguiuGravar = GravarSolicitante(id);

            if (conseguiuGravar)
                ApresentarMensagem("Solicitante editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar editar o solicitante", TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um solicitante...");

            VisualizarRegistros();

            Console.WriteLine();

            Console.Write("Digite o número do solicitante que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool conseguiuExcluir = controladorSolicitante.ExcluirSolicitante(idSelecionado);

            if (conseguiuExcluir)
                ApresentarMensagem("Equipamento excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o equipamento", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public override void VisualizarRegistros()
        {
            ConfigurarTela("Visualizando solicitante...");

            string configuracaColunasTabela = "{0,-10} | {1,-30} | {2,-55} | {3,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela);

            Solicitante[] solicitantes = controladorSolicitante.SelecionarTodosSolicitantes();

            if (solicitantes.Length == 0)
            {
                ApresentarMensagem("Nenhum solicitante cadastrado!", TipoMensagem.Atencao);
                return;
            }

            for (int i = 0; i < solicitantes.Length; i++)
            {
                Console.WriteLine(configuracaColunasTabela,
                    solicitantes[i].id, solicitantes[i].nome, solicitantes[i].email, solicitantes[i].telefone);
            }
        }

        #region métodos privados
        private static void MontarCabecalhoTabela(string configuracaColunasTabela)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(configuracaColunasTabela, "Id", "Equipamento", "Título", "Dias em Aberto");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        private bool GravarSolicitante(int id)
        {
            string resultadoValidacao;
            bool conseguiuGravar = true;

            Console.Write("Digite o nome do solicitante: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o número de telefone: ");
            string numeroTelefone = Console.ReadLine();

            Console.Write("Digite o email do solicitante: ");
            string email = Console.ReadLine();

            resultadoValidacao = controladorSolicitante.RegistrarSolicitante(id, nome, email, numeroTelefone);

            if (resultadoValidacao != "SOLICITANTE_VALIDO")
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                conseguiuGravar = false;
            }

            return conseguiuGravar;
        }
        #endregion
    }
}
