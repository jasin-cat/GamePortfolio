# AccelerateMovement
## Common
FadeControllerクラスでシングルトンを使用したいと思ったので、シングルトンを継承できるように、ジェネリックの基底クラスSingletonを作成しました。
###URL
https://github.com/jasin-cat/GamePortfolio/tree/main/Accelerate-Movement/Scripts/Common
## Enemy
Enemyの攻撃の動きを作りました。<br>
Enemyが攻撃する際攻撃範囲を表す線を出すという要望のだったので、<br>
shaderを使って、攻撃範囲と攻撃までの時間を線で表せれるようにしました。<br>
また、Enemyは多く生成されるオブジェクトだったので、オブジェクトプールを使って生成と破棄をあまりしないようにしました。
### URL
https://github.com/jasin-cat/GamePortfolio/tree/main/Accelerate-Movement/Scripts/Enemy
## Ranking
ランキングの実装をしました。
20位までのタイム、日付を保存して、スクロールできるようにしてほしい、また1～20位に入っていた場合は、演出をつけるという要望だったので、<br>
保存する形式はjsonファイルで保存をするようにして、表示は順位、タイム、日付をまとめた、RankingElementクラスを使って表示するようにしました。
### URL
https://github.com/jasin-cat/GamePortfolio/tree/main/Accelerate-Movement/Scripts/Ranking
## Shader
EnemyAttackShader.shaderでは、Enemyで使う撃範囲と攻撃までの時間をshaderで表現しました。<br>
FadeShader.shaderでは、ルール画像を使ってフェードを行えるようにshaderを書きました。
### URL
https://github.com/jasin-cat/GamePortfolio/tree/main/Accelerate-Movement/Scripts/Shader
