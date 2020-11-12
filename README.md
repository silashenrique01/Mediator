# Intuitive - Sistema de Cadastro e Pesquisa de usuários

## Configuração do Projeto

Para rodar o projeto é necessário ter instalado a ferramenta Angular CLI no seu computador.

Abra o cmd na pasta raiz e execute o seguinte comando 
```
npm i @angular/cli

```
## Estrutura do Projeto

O projeto esta divido em 4 camadas:

  1. Camada da API
      Contém as controllers e os objetos de transferência de dados(DTOs)
      Camada mais externa do Projeto, ela que recebe as chamadas do aplicativo angular
      "É como se fosse o cérebro da aplicação, redireciona para os lugares certos e retorna os valores"
      
  2. Camada de Dominío(Domain)
      Contém todas as entidades(Parece com uma Model) e regras de negócio do projeto --> Por conter as regras de negócio, deve ser a camada mais segura
      
  3. Camada de Repositório(Repository)
      Faz as consultas ao banco 
      
  4. Camada do Aplicativo Angular
      Parte de front que faz as chamadas da api


Antigamente o MVC era o padrão mais usado no desenovolvimento de software, porém surgiu a necessidade de separar o projeto em várias partes com responsabilidades exclusivas. 



## Banco de dados

  Atualmente salvei os dados em um SqlLite, mais simples e mais de boas de usar. Mas no arquivo eles pediram para usar o SqlServer, então é uma boa você tentar mudar.
  
  
  1. Acesse o arquivo Startup.cs da api e ache o método ConfigureServices

  ```
   public void ConfigureServices(IServiceCollection services)
          {
              services.AddDbContext<ProAgilContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
              ...
          }

  ```
  
  Você vai precisar instalar o pacote nuget do SqlServer e adicionar a string de conexão. Não lembro de cabeça como faz, mas tem bastante tutoriais na internet.
  
  
  ## Gerar o Banco
  
  Você vai precisar criar uma nova migration, então exclua a pasta "Migrations" que fica dentro do projeto repository.Agora abra o terminal e entre no diretório Intuitive.Repository. Assim: 
  
      ```
        cd Intuitive.Repository
      ```
        
    e digite dotnet ef --startup-project ..\Intuitive.API\ migrations add init 

    
    Uma nova pasta chamada Migrations deve ter sido criada no repository
    
    Agora digite
    
     ```
      dotnet ef --startup-project ..\Intuitive.API\ update database 
    
     ```
     
## Rodar Projeto

Rodar API
  ```
    cd Intuitive.Api
    dotnet run
  ```
  
 Rodar App Angular
  ```
    cd Intuitive-App
    npx ng serve ou ng serve
  ```
 

    
        
    
  



  
  




