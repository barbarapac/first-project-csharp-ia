# First Project C# IA

API web simples em ASP.NET Core que integra modelos de IA usando Microsoft.Extensions.AI.

## Funcionalidades

- **Endpoint `/`**: Chat básico com IA
- **Endpoint `/v2`**: Chat com contexto (especialista em clima)
- **Endpoint `/v3`**: Chat com cache para otimização

## Tecnologias

- .NET 10.0
- ASP.NET Core Web API
- Microsoft.Extensions.AI
- OllamaSharp (desenvolvimento)
- OpenAI GPT-4o-mini (produção)

## Configuração

### Desenvolvimento
- Usa Ollama local (http://localhost:11434) com modelo `phi3:latest`

### Produção
- Usa OpenAI GPT-4o-mini
- Configure a chave da OpenAI na variável `openAiKey`

## Como usar

Execute a aplicação e faça requisições POST:

```json
{
  "prompt": "Sua pergunta aqui"
}
```

## Execução

```bash
dotnet run
```