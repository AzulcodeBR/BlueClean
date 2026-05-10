using System.Text.Json.Serialization;

namespace BlueCleanApi.Domains.Dtos.Endereco
{
    /// <summary>
    /// DTO de resposta da consulta de CEP do ViaCEP
    /// </summary>
    public class CepResponseDto
    {
        /// <summary>
        /// CEP consultado
        /// </summary>
        /// <example>01001-000</example>
        [JsonPropertyName("cep")]
        public string Cep { get; set; } = string.Empty;

        /// <summary>
        /// Logradouro (Rua, Avenida, etc)
        /// </summary>
        /// <example>Praça da Sé</example>
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; } = string.Empty;

        /// <summary>
        /// Complemento do endereço
        /// </summary>
        /// <example>lado ímpar</example>
        [JsonPropertyName("complemento")]
        public string Complemento { get; set; } = string.Empty;

        /// <summary>
        /// Unidade (ex: unidade administrativa)
        /// </summary>
        [JsonPropertyName("unidade")]
        public string Unidade { get; set; } = string.Empty;

        /// <summary>
        /// Bairro
        /// </summary>
        /// <example>Sé</example>
        [JsonPropertyName("bairro")]
        public string Bairro { get; set; } = string.Empty;

        /// <summary>
        /// Cidade/Localidade
        /// </summary>
        /// <example>São Paulo</example>
        [JsonPropertyName("localidade")]
        public string Localidade { get; set; } = string.Empty;

        /// <summary>
        /// Unidade Federativa (Estado)
        /// </summary>
        /// <example>SP</example>
        [JsonPropertyName("uf")]
        public string Uf { get; set; } = string.Empty;

        /// <summary>
        /// Código do IBGE do município
        /// </summary>
        /// <example>3550308</example>
        [JsonPropertyName("ibge")]
        public string Ibge { get; set; } = string.Empty;

        /// <summary>
        /// Código GIA (Guia de Informação e Apuração do ICMS - SP)
        /// </summary>
        /// <example>1004</example>
        [JsonPropertyName("gia")]
        public string Gia { get; set; } = string.Empty;

        /// <summary>
        /// Código DDD da região
        /// </summary>
        /// <example>11</example>
        [JsonPropertyName("ddd")]
        public string Ddd { get; set; } = string.Empty;

        /// <summary>
        /// Código SIAFI (Sistema Integrado de Administração Financeira)
        /// </summary>
        /// <example>7107</example>
        [JsonPropertyName("siafi")]
        public string Siafi { get; set; } = string.Empty;

        /// <summary>
        /// Indica se houve erro na consulta
        /// </summary>
        [JsonPropertyName("erro")]
        public bool? Erro { get; set; }
    }
}
