# 获取 Commit Id
$gitCommit = git rev-list --max-count=1 HEAD
echo "Commit Id: $gitCommit"

# 获取运行时信息
$runtimeStr = $args[0]
if ($runtimeStr.length -gt 0) {
    $runtimeArg = "--runtime"
} else {
    $runtimeArg = ""
}

# 编译
dotnet restore
dotnet build Server --configuration Release --version-suffix "$gitCommit-$runtimeStr" $runtimeArg $runtimeStr --output Build/Server

# 收集
if (-Not (Test-Path ".\Build\Final"))
{
     md -path ".\Build\Final"
}
Copy-Item -Path ".\Build\Server\*" -Destination ".\Build\Final" -Recurse -Force
