using System.Text.Json.Serialization;

namespace CareerApplication.Core;

[Serializable]
public class FirebaseSettings
{
    [JsonPropertyName("type")]
    public string Type => "service_account";

    [JsonPropertyName("project_id")]
    public string ProjectId => "career-application-a7a65";

    [JsonPropertyName("api_key")]
    public string ApiKey => "AIzaSyBs8WQkHxzUTWJfFEAlPInZb21uih2Ex5Y";

    [JsonPropertyName("private_key_id")]
    public string PrivateKeyId => "08b916db94a7ad205a40c4354b664d0be3f528b4";

    [JsonPropertyName("private_key")]
    public string PrivateKey => "-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDJocs+8bzvn2x4\nL2rxOGxXNs0XsHqevGoD6Xsa6xrW+iTXsk2XJVt/Z90/wgli8ElcfAs7C8JT+9E1\nOuS19UULZliM6Z5R8BFvYZiZcATrWIDpj7oYmANjPWmiCgw70vRpx0itxlZ7qwJE\nCa+1kTCcHSzyGUGRvQQoZ8S/ffNJPoN13HMBneS1948gyboKv9H43q8jY9hgP7M0\nFsWoYHEr4hTjrnqRhVTAZyE4AmLcmWkW1tf7xoukaUMslicgeWwBkJsNhUGCPoaE\ngHlozZMPXKctxjgx8McAqs094aZs467lprDSq9L1UdqAPCVerL5UDqmsBqX3/Nbo\n3xj2U319AgMBAAECggEAIzKvyMKpYUk6xbY9XHOlkgn6fn6rwScR+LHa8CWHj2ym\nXtL88Xwvf0ux1033loJsxAzRZxLT7xY//KNVxp/U2DsbDSpBHXujh4aJfYuNCEpw\nYpVY/q8WtE3FsWhAn3bhfM+xk8hSU49UO21Xc4IvcqkzLeYTyK2u2qDWuzjw7eGA\n+OTOnyQ0dDS7hSi7mF0bEmze/gf61X3W+eJiR0d8cqiClqgvHfsAOqcCmuLg+n/O\n1A6wqF6oa6qW/bzvJqfeh0HlTsAxH6cJY05/ZgwuYxAj42qV8xvciB7kni2Rm0ca\nQl15eoK4aEiztRjjM9oOFEYEPTmt/QCBIII85OwQQQKBgQDoTIeXpbKWzJSgY0vt\nMIFO45snlYCpaaeOnL7vIq+ktzPcDQhCdy3IwHZl9aS2tX8onw+p8J2o/8mvvpHN\nk3/shpr9Cdsgpb9Zh626bmVR/SLr0QymXb1Uh5Dnj9jvJrvNl9tJDMpbxXh2Z1+Y\ns88q4H5R2r4zxiETdTJFUiEiHQKBgQDeNER0VpBXVY/qb2QpfVqviJgjL52BbtqL\n3ZYnPgjhFXUq0NYh8kNE2YxBLLHKD9Xq25xsqc2VBbzdB8efQH2jnaI9i5pJ29r/\nVOC87f9jM4hUuCJRVcbzazq1Cud1Nd9BgHYTVcTv64UH/9TEUvEifJzw2JGDITuW\nrQeLNz7q4QKBgARFa/TQ2iDFsDHmwX2h966wpqiqqgvzYl5P6X7SH3Ki37Q7FRzL\nvi7DwymKw7WMTsM7cXlJcEysL1Qo59tky+RYcT8X0EygX41Q3lPgNcK1KdA2Lspc\nTN/iAjsPvQhgqPFVynVkY9AeKGHc8rgHDlcWe5wbroWbLf6CmCMIKI3NAoGBAIbb\nk/xDmUDX9gtjZZfks4g7T5JzUJWAkPU4G+xfMwZgfobozj02Cmub2QqpNJIdAEiU\n1qAAgnwCSnifQyE9A8OsZQ4TTyWqYIR8oBUqIMi1OvWn93QDqkaL+spr/sPdWU0o\nyQC9GlQ+TbPoKdqpOB4lIR7vYYkDP2YYR67PbckhAoGBAJxILtyQL3jD22YR0t/p\nPrakS8C/DaHqvIdqMjvHHlvcQEXEB4U711KwvHarVAxwO+z4caBtQY+HnC1xnodo\n4yhwFTqv1be6sOGgvVUxtHDN2BzuNccOaLPFfrpNHQPieP++KgnoGp3c4oTsTfr5\nw2ys9srSvt9d8sePjSz0bTXK\n-----END PRIVATE KEY-----\n";

    [JsonPropertyName("client_email")]
    public string ClientEmail => "firebase-adminsdk-lmlph@career-application-a7a65.iam.gserviceaccount.com";

    [JsonPropertyName("client_id")]
    public string ClientId => "110464478302851296410";

    [JsonPropertyName("auth_uri")]
    public string AuthUri => "https://accounts.google.com/o/oauth2/auth";

    [JsonPropertyName("token_uri")]
    public string TokenUri => "https://oauth2.googleapis.com/token";

    [JsonPropertyName("auth_provider_x509_cert_url")]
    public string AuthProviderX509CertUrl => "https://www.googleapis.com/oauth2/v1/certs";

    [JsonPropertyName("client_x509_cert_url")]
    public string ClientX509CertUrl => "https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-lmlph%40career-application-a7a65.iam.gserviceaccount.com";

    [JsonPropertyName("realtime_database_url")]
    public string RealtimeDatabaseUrl => "https://career-application-a7a65-default-rtdb.firebaseio.com/";
}
