namespace WEB.AppInfraStructure.Utils.FileSystem.Wrappers {

    public static class RequestMethods {
        /// <summary>
        ///   Representa os tipos de m�todos de protocolo FTP que podem ser usados com uma solicita��o de FTP.
        ///    Essa classe n�o pode ser herdada.
        /// </summary>
        public static class Ftp {
            /// <summary>
            ///   Representa o m�todo de protocolo FTP RETR usado para baixar um arquivo de um servidor FTP.
            /// </summary>
            public const string DownloadFile = "RETR";

            /// <summary>
            ///   Representa o m�todo de protocolo FTP NLIST que obt�m uma lista resumida dos arquivos em um servidor FTP.
            /// </summary>
            public const string ListDirectory = "NLST";

            /// <summary>
            ///   Representa o m�todo do protocolo FTP STOR que carrega um arquivo para um servidor FTP.
            /// </summary>
            public const string UploadFile = "STOR";

            /// <summary>
            ///   Representa o m�todo de protocolo FTP DELE usado para excluir um arquivo de um servidor FTP.
            /// </summary>
            public const string DeleteFile = "DELE";

            /// <summary>
            ///   Representa o m�todo de protocolo FTP APPE usado para acrescentar um arquivo a outro existente em um servidor FTP.
            /// </summary>
            public const string AppendFile = "APPE";

            /// <summary>
            ///   Representa o m�todo do protocolo FTP SIZE usado para recuperar o tamanho de um arquivo em um servidor FTP.
            /// </summary>
            public const string GetFileSize = "SIZE";

            /// <summary>
            ///   Representa o protocolo FTP STOU que carrega um arquivo com um nome exclusivo em um servidor FTP.
            /// </summary>
            public const string UploadFileWithUniqueName = "STOU";

            /// <summary>
            ///   Representa o m�todo de protocolo FTP MKD que cria um diret�rio em um servidor FTP.
            /// </summary>
            public const string MakeDirectory = "MKD";

            /// <summary>
            ///   Representa o m�todo de protocolo FTP RMD que remove um diret�rio.
            /// </summary>
            public const string RemoveDirectory = "RMD";

            /// <summary>
            ///   Representa o m�todo de protocolo FTP NLIST que obt�m uma lista detalhada dos arquivos em um servidor FTP.
            /// </summary>
            public const string ListDirectoryDetails = "LIST";

            /// <summary>
            ///   Representa o m�todo de protocolo FTP MDTM usado para recuperar o carimbo de data/hora de um arquivo em um servidor FTP.
            /// </summary>
            public const string GetDateTimestamp = "MDTM";

            /// <summary>
            ///   Representa o m�todo do protocolo FTP PWD que imprime o nome do diret�rio de trabalho atual.
            /// </summary>
            public const string PrintWorkingDirectory = "PWD";

            /// <summary>
            ///   Representa o m�todo do protocolo FTP RENAME que renomeia um diret�rio.
            /// </summary>
            public const string Rename = "RENAME";
        }

        /// <summary>
        ///   Representa os tipos de m�todos de protocolo HTTP que podem ser usados com uma solicita��o HTTP.
        /// </summary>
        public static class Http {
            /// <summary>Representa um m�todo de protocolo HTTP GET.</summary>
            public const string Get = "GET";

            /// <summary>
            ///   Representa um m�todo de protocolo HTTP POST que � usado para postar uma nova entidade como uma adi��o a um URI.
            /// </summary>
            public const string Post = "POST";

            /// <summary>
            ///   Representa um m�todo de protocolo HTTP PUT � usado para substituir uma entidade identificada por um URI.
            /// </summary>
            public const string Put = "PUT";

            /// <summary>
            ///   Representa um m�todo de protocolo HTTP DELETE � usado para Excluir uma entidade identificada por um URI.
            /// </summary>
            public const string Delete = "DELETE";

            /// <summary>
            ///   Representa um m�todo de protocolo HTTP PATCH � usado para substituir uma campos espec�ficos de uma entidade identificada por um URI.
            /// </summary>
            public const string Patch = "PATCH";

            /// <summary>
            ///   Representa o m�todo do protocolo HTTP CONNECT usado com um proxy que pode mudar dinamicamente para t�nel, como no caso do t�nel SSL.
            /// </summary>
            public const string Connect = "CONNECT";

            /// <summary>
            ///   Representa um m�todo de protocolo HTTP HEAD.
            ///    O m�todo HEAD � id�ntico a GET, exceto que o servidor retorna apenas os cabe�alhos da mensagem na resposta, sem um corpo de mensagem.
            /// </summary>
            public const string Head = "HEAD";

            /// <summary>
            ///   Representa uma solicita��o HTTP MKCOL que cria uma nova cole��o (como uma cole��o de p�ginas) no local especificado pelo solicita��o Uniform Resource Identifier (URI).
            /// </summary>
            public const string MkCol = "MKCOL";
        }

        /// <summary>
        ///   Representa os tipos de m�todos de protocolo de arquivo que podem ser usados com uma solicita��o de ARQUIVO.
        ///    Essa classe n�o pode ser herdada.
        /// </summary>
        public static class File {
            /// <summary>
            ///   Representa o m�todo de protocolo FILE GET usado para recuperar um arquivo de um local especificado.
            /// </summary>
            public const string DownloadFile = "GET";

            /// <summary>
            ///   Representa o m�todo de protocolo arquivo PUT � usado para copiar um arquivo para um local especificado.
            /// </summary>
            public const string UploadFile = "PUT";
        }
    }

}