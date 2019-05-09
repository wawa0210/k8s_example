### 开发环境

Visual Studio 2017  
Linux Ubantu 服务器 一台

### 运行

1、下载源代码到本地

2、使用Visual Studio 2017打开，先在本地编译一下程序是否存在问题，编译通过后即可在将程序源代码上传到服务器，这里可以
  把bin文件目录排除掉。

3、windows电脑使用ftp工具进行上传，这里采用的[WinSCP](https://winscp.net/eng/download.php)

4、进入到服务器源代码所在的目录，使用docker编译源代码输出镜像，这里前置条件先确认服务器已经安装了docker。
至于docker的安装在这里不再赘述。不知道的可移步到这里 -- [docker安装](https://phoenixnap.com/kb/how-to-install-docker-on-ubuntu-18-04)

```bash
docker build -t k8s .

```

5、通过控制台可以看到整个源代码构建镜像的过程，也可以理解到image都是分层的，每层依次去构建的过程。

6、构建成功以后，可以看到控制台会输出

Successfully built a9f77d69f914

Successfully tagged k8s:latest

后面的字符串是构建成功后镜像的id

7、查看存在镜像，可以看到刚才构建的镜像在列表当中。

```
docker image ls

```
8、启动镜像
```
docker run -d --rm -p 9000:80  --name k8s k8s

```

9、可以看到镜像启动以后，通过docker ps 查看到镜像处于up的状态。

10、curl cip.cc 查看下当前服务器的外网ip，这里服务器外网ip为182.61.23.228。
在浏览器中输入 http://182.61.23.228:9000/api/values ，其中的端口就是刚才启动镜像时候设置的端口。可以看到返回的结果。

[

"value1",

"value2"

]



### Dockerfile 

核心的要理解Dockerfile里面的命令

```
FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src

COPY . /src/k8s_demo


WORKDIR /src/k8s_demo
RUN dotnet restore
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "k8s_demo.dll"]

```

> 第一段是运行时环境

> 第二段是编译环境  

> 第三段是restore程序的依赖项等

> 第四段发布程序到app目录

> 第五段是设置镜像启动后执行dotnet k8s_demo.dll

### 反馈

有问题可以随时提出issue进行交流反馈。

### License
[MIT](https://choosealicense.com/licenses/mit/)