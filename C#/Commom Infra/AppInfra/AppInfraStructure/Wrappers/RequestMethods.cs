namespace WEB.AppInfraStructure.Utils.FileSystem.Wrappers {

    public static class RequestMethods {
        /// <summary>
        ///   Representa os tipos de métodos de protocolo FTP que podem ser usados com uma solicitação de FTP.
        ///    Essa classe não pode ser herdada.
        /// </summary>
        public static class Ftp {
            /// <summary>
            ///   Representa o método de protocolo FTP RETR usado para baixar um arquivo de um servidor FTP.
            /// </summary>
            public const string DownloadFile = "RETR";

            /// <summary>
            ///   Representa o método de protocolo FTP NLIST que obtém uma lista resumida dos arquivos em um servidor FTP.
            /// </summary>
            public const string ListDirectory = "NLST";

            /// <summary>
            ///   Representa o método do protocolo FTP STOR que carrega um arquivo para um servidor FTP.
            /// </summary>
            public const string UploadFile = "STOR";

            /// <summary>
            ///   Representa o método de protocolo FTP DELE usado para excluir um arquivo de um servidor FTP.
            /// </summary>
            public const string DeleteFile = "DELE";

            /// <summary>
            ///   Representa o método de protocolo FTP APPE usado para acrescentar um arquivo a outro existente em um servidor FTP.
            /// </summary>
            public const string AppendFile = "APPE";

            /// <summary>
            ///   Representa o método do protocolo FTP SIZE usado para recuperar o tamanho de um arquivo em um servidor FTP.
            /// </summary>
            public const string GetFileSize = "SIZE";

            /// <summary>
            ///   Representa o protocolo FTP STOU que carrega um arquivo com um nome exclusivo em um servidor FTP.
            /// </summary>
            public const string UploadFileWithUniqueName = "STOU";

            /// <summary>
            ///   Representa o método de protocolo FTP MKD que cria um diretório em um servidor FTP.
            /// </summary>
            public const string MakeDirectory = "MKD";

            /// <summary>
            ///   Representa o método de protocolo FTP RMD que remove um diretório.
            /// </summary>
            public const string RemoveDirectory = "RMD";

            /// <summary>
            ///   Representa o método de protocolo FTP NLIST que obtém uma lista detalhada dos arquivos em um servidor FTP.
            /// </summary>
            public const string ListDirectoryDetails = "LIST";

            /// <summary>
            ///   Representa o método de protocolo FTP MDTM usado para recuperar o carimbo de data/hora de um arquivo em um servidor FTP.
            /// </summary>
            public const string GetDateTimestamp = "MDTM";

            /// <summary>
            ///   Representa o método do protocolo FTP PWD que imprime o nome do diretório de trabalho atual.
            /// </summary>
            public const string PrintWorkingDirectory = "PWD";

            /// <summary>
            ///   Representa o método do protocolo FTP RENAME que renomeia um diretório.
            /// </summary>
            public const string Rename = "RENAME";
        }

        /// <summary>
        ///   Representa os tipos de métodos de protocolo HTTP que podem ser usados com uma solicitação HTTP.
        /// </summary>
        public static class Http {
            /// <summary>Representa um método de protocolo HTTP GET.</summary>
            public const string Get = "GET";

            /// <summary>
            ///   Representa um método de protocolo HTTP POST que é usado para postar uma nova entidade como uma adição a um URI.
            /// </summary>
            public const string Post = "POST";

            /// <summary>
            ///   Representa um método de protocolo HTTP PUT é usado para substituir uma entidade identificada por um URI.
            /// </summary>
            public const string Put = "PUT";

            /// <summary>
            ///   Representa um método de protocolo HTTP DELETE é usado para Excluir uma entidade identificada por um URI.
            /// </summary>
            public const string Delete = "DELETE";

            /// <summary>
            ///   Representa um método de protocolo HTTP PATCH é usado para substituir uma campos específicos de uma entidade identificada por um URI.
            /// </summary>
            public const string Patch = "PATCH";

            /// <summary>
            ///   Representa o método do protocolo HTTP CONNECT usado com um proxy que pode mudar dinamicamente para túnel, como no caso do túnel SSL.
            /// </summary>
            public const string Connect = "CONNECT";

            /// <summary>
            ///   Representa um método de protocolo HTTP HEAD.
            ///    O método HEAD é idêntico a GET, exceto que o servidor retorna apenas os cabeçalhos da mensagem na resposta, sem um corpo de mensagem.
            /// </summary>
            public const string Head = "HEAD";

            /// <summary>
            ///   Representa uma solicitação HTTP MKCOL que cria uma nova coleção (como uma coleção de páginas) no local especificado pelo solicitação Uniform Resource Identifier (URI).
            /// </summary>
            public const string MkCol = "MKCOL";
        }

        /// <summary>
        ///   Representa os tipos de métodos de protocolo de arquivo que podem ser usados com uma solicitação de ARQUIVO.
        ///    Essa classe não pode ser herdada.
        /// </summary>
        public static class File {
            /// <summary>
            ///   Representa o método de protocolo FILE GET usado para recuperar um arquivo de um local especificado.
            /// </summary>
            public const string DownloadFile = "GET";

            /// <summary>
            ///   Representa o método de protocolo arquivo PUT é usado para copiar um arquivo para um local especificado.
            /// </summary>
            public const string UploadFile = "PUT";
        }
    }

}