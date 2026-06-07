# BlueClean API - Documentação

## 📋 Visão Geral

API RESTful para gerenciamento do sistema BlueClean, desenvolvida em .NET 10 com autenticação JWT.

## 🚀 Acessando a Documentação

Após iniciar o projeto, a documentação interativa estará disponível em:

- **Scalar UI**: `https://localhost:7165/scalar/v1`

A interface Scalar exibe:
- 📝 Descrição de cada endpoint
- 📦 Modelos de requisição e resposta
- 🔐 Suporte para autenticação JWT
- 💡 Exemplos de uso
- ⚡ Possibilidade de testar os endpoints diretamente

## 🔑 Autenticação

A API utiliza JWT (JSON Web Token) para autenticação.

### Como autenticar:

1. **Obter o token**:
   ```
   POST /api/Login/Autenticar
   {
	 "email": "usuario@blueclean.com",
	 "senha": "senhaSegura123"
   }
   ```

2. **Usar o token** nos endpoints protegidos:
   ```
   Authorization: Bearer {seu_token_jwt}
   ```

## 📚 Endpoints Disponíveis

### 🔓 Login
- `POST /api/Login/Autenticar` - Autentica um usuário e retorna token JWT

### 👤 Usuário
- `GET /api/Usuario/ObterUsuarioLogado` 🔒 - Obtém dados do usuário autenticado

🔒 = Requer autenticação

## 🛠️ Tecnologias

- .NET 10
- JWT Bearer Authentication
- Scalar para documentação
- OpenAPI 3.0

## 📖 Documentação XML

Todos os endpoints e modelos estão documentados com comentários XML, incluindo:
- Descrição da funcionalidade
- Parâmetros de entrada
- Códigos de resposta HTTP
- Exemplos de uso
- Observações importantes
