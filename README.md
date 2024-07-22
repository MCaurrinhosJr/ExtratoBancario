# BancoExtrato

**BancoExtrato** é uma aplicação web desenvolvida em .NET 8 utilizando o padrão MVC (Model-View-Controller). Este projeto visa fornecer uma interface para os correntistas de um banco visualizarem e gerenciarem seus extratos bancários de forma eficiente.

## Funcionalidades Principais

- **Visualização do Extrato Bancário:**
  - **Tela de Extrato:** Permite aos usuários visualizar suas transações filtradas por um intervalo de dias selecionado (5, 10, 15 ou 20 dias).
  - **Colunas do Extrato:** Mostra as transações com as seguintes informações:
    - Data (no formato dd/MM)
    - Tipo da Transação (Crédito/Débito)
    - Valor Monetário

- **Geração e Compartilhamento de PDF:**
  - **Botão de Compartilhamento:** Gera um arquivo PDF com o extrato bancário filtrado para impressão ou compartilhamento.
  - **Geração de PDF:** Utiliza a biblioteca PdfSharp para criar PDFs com base nas transações filtradas.

## Tecnologias Utilizadas

- **Backend:** .NET 8 com padrão MVC (Model-View-Controller).
- **Banco de Dados:** SQL Server, gerenciado através do Entity Framework Core.
- **Documentação e Testes de API:** Swagger.
- **Geração de PDF:** PdfSharp.

## Requisitos

- **.NET 8:** Para a execução da aplicação.
- **SQL Server:** Para armazenamento de dados.
- **Swagger:** Para documentação e teste dos endpoints da API.
- **PdfSharp:** Para criação de arquivos PDF.

## Objetivos

- Facilitar a visualização dos extratos bancários de forma clara e eficiente.
- Permitir a geração e o compartilhamento de extratos em formato PDF.
- Seguir as melhores práticas de desenvolvimento e arquitetura de software.

## Testes

- **Testes Unitários:** Verificam a lógica de negócios, serviços e controladores.
- **Testes de Integração:** Garantem que os endpoints da API e a interação com o banco de dados funcionem conforme o esperado.

## Como Rodar o Projeto

1. Clone este repositório:
   ```bash
   git clone https://github.com/MCaurrinhosJr/ExtratoBancario.git
