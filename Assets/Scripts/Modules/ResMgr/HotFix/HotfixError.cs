namespace GameSets
{
    public class HotfixError
    {
        public static HotfixErrorType HotfixErrorType { get; set; }
    }
    
    public enum HotfixErrorType
    {
        None,
        RemoteVersionCfgError, // 没有远程配置
        RemoteVersionItemError, // 没有远程VersionItem
        RemoteAssetVersionsError, // 没有远程AssetVersions
        DownLoadFileError, // 下载失败
        WriteFileError, // 写入文件失败
        WriteRemoteVersionCfgError, // 写入远程配置失败
        WriteRemoteAssetVersionsError, // 写入远程资源json失败
    }
}