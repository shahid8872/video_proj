pipeline {
    agent any

    environment {
        DOTNET_ROOT = "C:\\Program Files\\dotnet"
        PATH = "${env.DOTNET_ROOT};${env.PATH}"
        IMAGE_NAME = "video_proj-api"
        CONTAINER_NAME = "video_proj_container"
        PROJECT_PATH = "video_proj/video_proj.csproj"
        PUBLISH_DIR = "published"
    }

    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/shahid8872/video_proj.git'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test --no-build --verbosity normal'
            }
        }

        stage('Publish') {
            steps {
                bat "dotnet publish ${env.PROJECT_PATH} -c Release -o ${env.PUBLISH_DIR}"
            }
        }

        stage('Docker Build') {
            steps {
                bat "docker build -t ${env.IMAGE_NAME} ./video_proj"
            }
        }

        stage('Docker Cleanup (Old Container)') {
            steps {
                script {
                    def stopCode = bat(script: "docker stop %CONTAINER_NAME%", returnStatus: true)
                    if (stopCode != 0) {
                        echo "No running container to stop (exit code ${stopCode})."
                    }

                    def rmCode = bat(script: "docker rm %CONTAINER_NAME%", returnStatus: true)
                    if (rmCode != 0) {
                        echo "No existing container to remove (exit code ${rmCode})."
                    }
                }
            }
        }

        stage('Docker Run') {
            steps {
                bat "docker run -d -p 8080:80 --name ${env.CONTAINER_NAME} ${env.IMAGE_NAME}"
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploy stage complete (customize if needed).'
            }
        }
    }

    post {
        failure {
            echo '❌ Build or deployment failed. Check logs above.'
        }
        success {
            echo '✅ Build and deployment succeeded!'
        }
    }
}
