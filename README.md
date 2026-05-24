# SOLIN API

Integrantes do grupo
Nome	RM
Rodrigo Silva	565162
Nickolas Davi	564105
Samara Vilela	566133
Natália Cristina	564099
Otávio Ferreira	565960

> "O cuidado que protege o seu pet"

API REST desenvolvida em ASP.NET Core para o sistema SOLIN, plataforma de monitoramento contínuo de saúde de pets com integração IoT e IA.

---

## Descrição do Projeto

A SOLIN resolve o principal problema dos apps de saúde animal: a falta de uso diário. O sistema coleta dados automaticamente via sensores IoT (sensor urinário com ESP32) e incentiva check-ins rápidos pelo app mobile, gerando um histórico real e contínuo do pet para acompanhamento veterinário.

---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- Oracle.EntityFrameworkCore
- Swashbuckle (Swagger + Annotations)
- Banco de Dados: Oracle (FIAP)

---

## Instalação e Execução

### Pré-requisitos

- .NET 8 SDK
- Visual Studio 2022
- Credenciais Oracle fornecidas pela FIAP

### Passo a passo

**1. Clone o repositório**
```bash
git clone https://github.com/seu-usuario/SolinAPI.git
cd SolinAPI
```

**2. Configure a string de conexão**

Abra o `appsettings.json` e substitua `SEU_USUARIO` e `SUA_SENHA` pelas credenciais Oracle da FIAP:

```json
"ConnectionStrings": {
  "Oracle": "Data Source=...;User Id=SEU_USUARIO;Password=SUA_SENHA"
}
```

**3. Instale os pacotes NuGet**

No Visual Studio, vá em Tools → NuGet Package Manager → Package Manager Console:

```powershell
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Oracle.EntityFrameworkCore
Install-Package Swashbuckle.AspNetCore
Install-Package Swashbuckle.AspNetCore.Annotations
```

**4. Execute as Migrations**

No Package Manager Console:

```powershell
Add-Migration Inicial
Update-Database
```

**5. Execute o projeto**

Pressione F5 no Visual Studio ou rode:

```bash
dotnet run
```

**6. Acesse o Swagger**

Abra no navegador: `http://localhost:{porta}/swagger`

---

## Documentação das Rotas

### Tutor — /api/tutor

| Método | Rota | Descrição | Status |
|---|---|---|---|
| GET | /api/tutor | Lista todos os tutores | 200 |
| GET | /api/tutor/{id} | Busca tutor por ID | 200 / 404 |
| GET | /api/tutor/{id}/pets | Lista os pets de um tutor | 200 / 404 |
| POST | /api/tutor | Cria novo tutor | 201 / 400 |
| PUT | /api/tutor/{id} | Atualiza tutor | 200 / 404 |
| DELETE | /api/tutor/{id} | Remove tutor | 204 / 404 |

### Pet — /api/pet

| Método | Rota | Descrição | Status |
|---|---|---|---|
| GET | /api/pet | Lista todos os pets | 200 |
| GET | /api/pet/{id} | Busca pet por ID | 200 / 404 |
| GET | /api/pet/{id}/eventos | Lista eventos do pet | 200 / 404 |
| GET | /api/pet/{id}/historico | Histórico de saúde do pet | 200 / 404 |
| GET | /api/pet/{id}/alertas | Alertas do pet | 200 / 404 |
| GET | /api/pet/{id}/eventos/{tipo} | Eventos por tipo | 200 / 404 |
| POST | /api/pet | Cria novo pet | 201 / 400 |
| PUT | /api/pet/{id} | Atualiza pet | 200 / 404 |
| DELETE | /api/pet/{id} | Remove pet | 204 / 404 |

Tipos de evento: `CHECK_IN`, `PASSEIO`, `SENSOR_URINARIO`, `ALIMENTACAO`

### Evento — /api/evento

| Método | Rota | Descrição | Status |
|---|---|---|---|
| GET | /api/evento | Lista todos os eventos | 200 |
| GET | /api/evento/{id} | Busca evento por ID | 200 / 404 |
| POST | /api/evento | Registra novo evento | 201 / 400 |
| PUT | /api/evento/{id} | Atualiza evento | 200 / 404 |
| DELETE | /api/evento/{id} | Remove evento | 204 / 404 |

Origens: `APP`, `IOT`

### Historico — /api/historico

| Método | Rota | Descrição | Status |
|---|---|---|---|
| GET | /api/historico | Lista todos os históricos | 200 |
| GET | /api/historico/{id} | Busca histórico por ID | 200 / 404 |
| POST | /api/historico | Cria registro de saúde | 201 / 400 |
| PUT | /api/historico/{id} | Atualiza histórico | 200 / 404 |
| DELETE | /api/historico/{id} | Remove histórico | 204 / 404 |

Categorias: `CONSULTA`, `VACINA`, `CIRURGIA`, `EXAME`, `MEDICAMENTO`

### Alerta — /api/alerta

| Método | Rota | Descrição | Status |
|---|---|---|---|
| GET | /api/alerta | Lista todos os alertas | 200 |
| GET | /api/alerta/{id} | Busca alerta por ID | 200 / 404 |
| GET | /api/alerta/pet/{idPet}/nao-lidos | Alertas não lidos de um pet | 200 / 404 |
| POST | /api/alerta | Cria novo alerta | 201 / 400 |
| PUT | /api/alerta/{id} | Atualiza alerta | 200 / 404 |
| DELETE | /api/alerta/{id} | Remove alerta | 204 / 404 |

Severidades: `BAIXA`, `MEDIA`, `ALTA`

---

## Estrutura do Banco de Dados

```
tb_tutor       → cadastro dos tutores
tb_pet         → cadastro dos pets (vinculado ao tutor)
tb_evento      → eventos do pet (check-in, passeio, sensor IoT)
tb_historico   → histórico de saúde veterinária
tb_alerta      → alertas gerados por regras automáticas
```

---

## Equipe

Projeto desenvolvido para a disciplina de Advanced Business Development with .NET — FIAP.

Plataforma: SOLIN — Sistema de monitoramento de saúde de pets com IoT e IA.
