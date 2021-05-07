﻿using GestaoEquipamentos.ConsoleApp.Dominio;
using System;

namespace GestaoEquipamentos.ConsoleApp.Controladores
{
    public class ControladorChamado : ControladorBase
    {
        private ControladorEquipamento controladorEquipamento;
        private ControladorSolicitante controladorSolicitante;

        public ControladorChamado(ControladorEquipamento controlador, ControladorSolicitante controladorSol)
        {
            controladorEquipamento = controlador;
            controladorSolicitante = controladorSol;
        }

        public string RegistrarChamado(int id, int idEquipamentoChamado,
            string titulo, string descricao, DateTime dataAbertura, int idSolicitanteChamado)
        {
            Chamado chamado = null;

            int posicao;

            if (id == 0)
            {
                chamado = new Chamado();
                posicao = ObterPosicaoVaga();
            }
            else
            {
                posicao = ObterPosicaoOcupada(new Equipamento(id));
                chamado = (Chamado)registros[posicao];
            }

            chamado.solicitante = controladorSolicitante.SelecionarSolicitantePorId(idSolicitanteChamado);
            chamado.equipamento = controladorEquipamento.SelecionarEquipamentoPorId(idEquipamentoChamado);
            chamado.titulo = titulo;
            chamado.descricao = descricao;
            chamado.dataAbertura = dataAbertura;

            string resultadoValidacao = chamado.Validar();

            if (resultadoValidacao == "CHAMADO_VALIDO")
                registros[posicao] = chamado;

            return resultadoValidacao;
        }

        public bool ExcluirChamado(int idSelecionado)
        {
            return ExcluirRegistro(new Chamado(idSelecionado));
        }

        public Chamado[] SelecionarTodosChamados()
        {
            Chamado[] chamadosAux = new Chamado[QtdRegistrosCadastrados()];

            Array.Copy(SelecionarTodosRegistros(), chamadosAux, chamadosAux.Length);

            return chamadosAux;
        }


    }
}
