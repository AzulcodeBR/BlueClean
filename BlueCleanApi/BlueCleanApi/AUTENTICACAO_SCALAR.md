# Como Configurar Autenticação JWT no Scalar

## 📋 Passos para Autenticar

### 1️⃣ Obter o Token JWT

Execute o endpoint de autenticação:

```http
POST /api/Login/Autenticar
Content-Type: application/json

{
  "email": "usuario@blueclean.com",
  "senha": "senhaSegura123"
}
```

**Resposta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "usuario@blueclean.com"
}
```

### 2️⃣ Configurar no Scalar

#### Opção A: Botão de Autenticação (Recomendado)
1. No topo da interface do Scalar, localize o botão de **autenticação** (ícone de cadeado) 🔒
2. Clique no botão
3. Selecione "Bearer Token" ou "HTTP Bearer"
4. Cole o token obtido (apenas o valor, sem "Bearer ")
5. Clique em "Save" ou "Salvar"

#### Opção B: Adicionar Manualmente no Endpoint
1. Ao testar um endpoint protegido, procure por "Authorization" ou "Headers"
2. Adicione o header:
   - **Nome:** `Authorization`
   - **Valor:** `Bearer {seu_token_aqui}`

### 3️⃣ Testar Endpoint Protegido

Agora você pode executar endpoints com `[Authorize]`:

```http
GET /api/Usuario/ObterUsuarioLogado
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## 🔐 Endpoints que Requerem Autenticação

| Endpoint | Método | Autenticação |
|----------|--------|--------------|
| `/api/Login/Autenticar` | POST | ❌ Não |
| `/api/Usuario/ObterUsuarioLogado` | GET | ✅ Sim (Bearer Token) |

## ⚠️ Observações

- O token expira em **60 minutos** (configurável em appsettings.json)
- Se receber erro 401, gere um novo token
- O token deve sempre começar com `Bearer ` quando enviado no header
- No Scalar, cole apenas o token sem o prefixo "Bearer "

## 🛠️ Troubleshooting

### Erro: "Token de autenticação não informado"
- Certifique-se de ter configurado a autenticação no Scalar
- Verifique se o header Authorization está sendo enviado

### Erro: "Token de autenticação inválido"
- Verifique se copiou o token completo
- Gere um novo token através do endpoint de autenticação

### Erro: "Token de autenticação expirado"
- O token tem validade de 60 minutos
- Gere um novo token através do endpoint `/api/Login/Autenticar`
