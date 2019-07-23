#!groovy

def errorMessage

def getGitUrl()
{
    return "https://github.com/takuya-takeuchi/DlibDotNet"
}

def getGitBranch()
{
    return "develop"
}

def getSource()
{
    def work
    def dlibDotNet
    def gitUrl = getGitUrl()
    def gitBranch = getGitBranch()

    if(isUnix())
    {
        work = env.WORKSPACE + "/work"
        dlibDotNet = work + "/DlibDotNet"

        echo 'work: ' + work
        echo 'dlibDotNet: ' + dlibDotNet

        if (!fileExists(work))
        {
            sh 'mkdir -p ' + work
        }

        if (!fileExists(dlibDotNet))
        {
            dir(work)
            {
                sh "git clone -b ${gitBranch} ${gitUrl}"
            }
        }
    }
    else
    {
        work = env.WORKSPACE + "\\work"
        dlibDotNet = work + "\\DlibDotNet"

        echo 'work: ' + work
        echo 'dlibDotNet: ' + dlibDotNet

        if (!fileExists(work))
        {
            bat 'mkdir ' + work
        }

        if (!fileExists(dlibDotNet))
        {
            dir(work)
            {
                bat "git clone -b ${gitBranch} ${gitUrl}"
            }
        }
    }

    return dlibDotNet
}

def initialize(root)
{
    def gitBranch = getGitBranch()

    dir(root)
    {
        if(isUnix())
        {
            sh 'git clean -fxd nuget'
            sh 'git checkout .'
            sh "git checkout ${gitBranch}"
            sh "git pull origin ${gitBranch}"
            sh './Initialize.sh'
        }
        else
        {
            bat 'git clean -fxd nuget'
            bat 'git checkout .'
            bat "git checkout ${gitBranch}"
            bat "git pull origin ${gitBranch}"
            bat 'Initialize.bat'
        }
    }
}

def preparation()
{
    def dlibDotNet = getSource()
    initialize(dlibDotNet)
}

def getNugetDir(dlibDotNet)
{
    if(isUnix())
    {
        return dlibDotNet + "/nuget"
    }
    else
    {
        return dlibDotNet + "\\nuget"
    }
}

def getDockerDir(dlibDotNet)
{
    if(isUnix())
    {
        return dlibDotNet + "/docker"
    }
    else
    {
        return dlibDotNet + "\\docker"
    }
}

def getArtifactsDir(dlibDotNet)
{
    if(isUnix())
    {
        return getNugetDir(dlibDotNet) + "/artifacts"
    }
    else
    {
        return getNugetDir(dlibDotNet) + "\\artifacts"
    }
}

def buildContainer()
{
    def dlibDotNet
    def buildWorkSpace

    stage("Initialize")
    {
        dlibDotNet = getSource()
        initialize(dlibDotNet)
    }

    stage('Build')
    {
        buildWorkSpace = getDockerDir(dlibDotNet)
        dir(buildWorkSpace)
        {
            if(isUnix())
            {     
                sh 'pwsh build_devel.ps1'  
                sh 'pwsh build_runtime.ps1'    
            }
            else
            {
                bat 'pwsh build_devel.ps1'  
                bat 'pwsh build_runtime.ps1'    
            }
        }   
    }
}

def build(script, stashName)
{
    echo 'script: ' + script
    echo 'stashName: ' + stashName

    def dlibDotNet
    def buildWorkSpace
    def artifactsSpace

    stage("Initialize")
    {
        dlibDotNet = getSource()
        initialize(dlibDotNet)
    }

    stage('Build')
    {
        buildWorkSpace = getNugetDir(dlibDotNet)
        artifactsSpace = getArtifactsDir(dlibDotNet)
        dir(buildWorkSpace)
        {
            if(isUnix())
            {     
                sh script    
            }
            else
            {
                bat script
            }
        }   
    }

    stage('Results')
    {
        dir(artifactsSpace)
        {
            stash stashName
        }
    }
}

def test(script, stashName, include)
{
    echo 'script: ' + script
    echo 'stashName: ' + stashName

    def dlibDotNet
    def buildWorkSpace
    def artifactsSpace

    stage("Initialize")
    {
        dlibDotNet = getSource()
        initialize(dlibDotNet)
    }

    stage('Test')
    {
        buildWorkSpace = getNugetDir(dlibDotNet)
        artifactsSpace = getArtifactsDir(dlibDotNet)

        dir(buildWorkSpace)
        {
            if(isUnix())
            {
                unstash 'nupkg'
                sh 'git checkout .'
                sh script
            }
            else
            {
                unstash 'nupkg'
                bat 'git checkout .'
                bat script
            }
        }
    }

    stage('Results')
    {
        dir(buildWorkSpace)
        {
            stash name: stashName, includes: include, excludes: '*.log'
        }
    }
}

node('master')
{
    try
    {
        def props
        stage("Preparation")
        {
            if(isUnix())
            {
                def file = env.JENKINS_HOME + '/DlibDotNet.json'
                props = readJSON file: file
            }
            else
            {
                def file = env.JENKINS_HOME + '\\DlibDotNet.json'
                props = readJSON file: file
            } 
        }
        
        stage("Build Container")
        {
            def nodeName = props['build']['linux-node']
            node(nodeName)
            {
                buildContainer()
            }
        }

        stage("Build")
        {
            def builders = [:]

            builders['windows'] =
            {
                def nodeName = props['build']['windows-node']
                node(nodeName)
                {
                    echo 'Build for Windows'
                    build('pwsh BuildWindows.ps1', 'windows')

                    echo 'Build for Universal Windows Application'
                    build('pwsh BuildUniversalWindowsPlatform.ps1', 'uwp')
                }
            }
            builders['linux'] =
            {
                def nodeName = props['build']['linux-node']
                node(nodeName)
                {
                    echo 'Build for Linux'
                    build('pwsh BuildUbuntu16.ps1', 'linux')
                }
            }
            builders['osx'] =
            {
                def nodeName = props['build']['osx-node']
                node(nodeName)
                {
                    echo 'Build for OSX'
                    withEnv(["PATH+LOCAL=/usr/local/bin"])
                    {
                        build('pwsh BuildOSX.ps1', 'osx')
                    }
                }
            }

            parallel builders
        }

        stage("Packaging")
        {
            def nodeName = props['packaging']['node']
            node(nodeName)
            {
                echo 'Get source code'
                def dlibDotNet = getSource()
                initialize(dlibDotNet)

                buildWorkSpace = getNugetDir(dlibDotNet)
                artifactsSpace = getArtifactsDir(dlibDotNet)

                echo 'Get artifacts'
                dir(artifactsSpace)
                {
                    unstash 'windows'
                    unstash 'uwp'
                    unstash 'linux'
                    unstash 'osx'
                }

                echo 'Create packages'
                dir(buildWorkSpace)
                {
                    stage('Build DlibDotNet Source')
                    {
                        bat 'BuildNuspec.Pre.bat'
                    }

                    stage('Build Native DlibDotNet Source')
                    {
                        parallel 'CPU':
                        {
                            stage('Build DlibDotNet.CPU')
                            {
                                bat 'pwsh CreatePackage.ps1 CPU'
                            }
                        }, 'CUDA': {
                            stage('Build DlibDotNet.CUDA')
                            {
                                bat 'pwsh CreatePackage.ps1 CUDA-92'
                                bat 'pwsh CreatePackage.ps1 CUDA-100'
                                bat 'pwsh CreatePackage.ps1 CUDA-101'
                            }
                        }, 'MKL': {
                            stage('Build DlibDotNet.MKL')
                            {
                                bat 'pwsh CreatePackage.ps1 MKL'
                            }
                        }, 'UWP': {
                            stage('Build DlibDotNet.UWP')
                            {
                                bat 'pwsh CreatePackage.ps1 UWP'
                            }
                        },'ARM': {
                            stage('Build DlibDotNet.ARM')
                            {
                                // bat 'pwsh CreatePackage.ps1 ARM'
                            }
                        }
                    }

                    // delete dll, so and dylib
                    echo 'Clean artifacts directory'
                    if(isUnix())
                    {
                        sh 'git clean -fxd artifacts'
                    }
                    else
                    {
                        bat 'git clean -fxd artifacts'
                    }

                    stash name: 'nupkg', includes: '**/*.nupkg'
                }
            }
        }

        stage("Test")
        {
            def builders = [:]
            def version = params.Version

            builders['windows'] =
            {
                def nodeName = props['test']['windows-node']
                node(nodeName)
                {
                    echo 'Test on Windows'
                    test("pwsh TestPackageWindows.ps1 ${version}", 'test-windows', 'artifacts/test/**/*.trx')

                    def thumbprint = props['test']['uwp']['thumbprint']
                    def certificateKeyFile = props['test']['uwp']['certificateKeyFile']
                    def notCountAsFailTests = props['test']['uwp']['windowsAppCertificationKit']['notCountAsFailTests']
                    echo 'Test on Universal Windows Application'
                    test("pwsh TestPackageUniversalWindowsPlatform.ps1 ${version} ${thumbprint} ${certificateKeyFile} ${notCountAsFailTests}", 'test-uwp', 'artifacts/test/**/*.xml')
                }
            }
            builders['linux'] =
            {
                def nodeName = props['test']['linux-node']
                node(nodeName)
                {
                    echo 'Test on Linux'
                    test("pwsh TestPackageUbuntu16.ps1 ${version}", 'test-linux', 'artifacts/test/**/*.trx')
                }
            }
            // builders['linux-arm'] =
            // {
            //     def nodeName = props['test']['linux-arm-node']
            //     node(nodeName)
            //     {
            //         echo 'Test on Linux-ARM'
            //         withEnv(["PATH+LOCAL=/usr/local/share/dotnet"])
            //         {
            //             test('./TestPackageRaspberryPi.sh ' + params.Version, 'test-linux-arm', 'artifacts/test/**/*.trx')
            //         }
            //     }
            // }
            builders['osx'] =
            {
                def nodeName = props['test']['osx-node']
                node(nodeName)
                {
                    echo 'Test on OSX'
                    withEnv(["PATH+LOCAL=/usr/local/bin:/usr/local/share/dotnet"])
                    {
                        test("pwsh TestPackageOSX.ps1 ${version}", 'test-osx', 'artifacts/test/**/*.trx')
                    }
                }
            }

            parallel builders
        }

        stage("result")
        {
            def nodeName = props['packaging']['node']
            node(nodeName)
            {
                dir(buildWorkSpace)
                {
                    unstash 'nupkg'
                    unstash 'test-windows'
                    unstash 'test-uwp'
                    unstash 'test-linux'
                    // unstash 'test-linux-arm'
                    unstash 'test-osx'

                    archiveArtifacts artifacts: 'artifacts/test/**/*.*'
                    archiveArtifacts artifacts: '*.nupkg'
                }
            }
        }
    }
    catch (err)
    {
        errorMessage = "${err}"
        currentBuild.result = "FAILURE"

        echo errorMessage
    }
    finally
    {
        if(currentBuild.result != "FAILURE")
        {
            currentBuild.result = "SUCCESS"
        }
    }
}