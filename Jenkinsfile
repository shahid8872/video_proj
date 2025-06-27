pipeline {
    agent any

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
                bat 'dotnet test'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o published'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploy step (add deployment commands here)'
            }
        }
    }

    post {
        failure {
            echo '❌ Build or deployment failed. Check logs above.'
        }
    }
}
