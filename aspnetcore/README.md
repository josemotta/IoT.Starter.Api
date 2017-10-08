# Teste para workflow API-based:  

- Restlet gera API e exporta swagger.json   
- SwaggerHub importa arquivo e gera server stub   
- Instala server stub em docker repo: io.swagger   
- Compila em máquina x64 e gera linux-arm: io.swagger   
- Docker push josemottalopes/io.swagger   
- Na Raspberry Pi, com hostname \"pi\", instala-se o Docker   
- Docker run -d josemottalopes/io.swagger   
- Retorna à maquina x64 e acessa o pi-server-stub via browser   
- http://pi:5000/swagger/ui/index.html    

