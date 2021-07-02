---
title: "為甚麼要做筆記，筆記的三種類型"
date: 2021-05-14T20:48:54+08:00
lastmod: 2021-05-14T20:48:54+08:00
draft: true
keywords: []
description: "筆記的三種類型"
tags: [learning, note]
category: "blog"
author: "angus chan"
featured_image: ""
listable: true
important: 1

# You can also close(false) or open(true) something for this content.
# P.S. comment can only be closed
comment: true
toc: true
autoCollapseToc: false
postMetaInFooter: false
hiddenFromHomePage: false
# You can also define another contentCopyright. e.g. contentCopyright: "This is another copyright."
contentCopyright: false
reward: false
mathjax: false
mathjaxEnableSingleDollar: false
mathjaxEnableAutoNumber: false

# You unlisted posts you might want not want the header or footer to show
hideHeaderAndFooter: false

# You can enable or disable out-of-date content warning for individual post.
# Comment this out to use the global config.
#enableOutdatedInfoWarning: false

flowchartDiagrams:
  enable: false
  options: ""

sequenceDiagrams: 
  enable: false
  options: ""

---



~~~~~
為甚麼要寫筆記

簡單解說筆記的三種類型類型
  抄寫 - 用於紀錄大量繁瑣資訊，方便之後翻找
  彙整 - 將繁瑣零碎的資訊用自己的解方式重新描述
  舉例 - 釐清資訊間的因果關西，並舉出例子加以驗證

針對每種類型的詳解
  抄寫
    最無效的學習方式，為什麼無效
  彙整
    真正有效的學習方式，為什麼有效
    如何彙整
    想像成在對人解說，總不可能說(前題 frontmatter (YAML, TOML, JSON)可以有設置如語言的東西)這種不明不白的東西，這只有自己看得懂而已
  舉例
    最有效的學習，為什麼?
    如何舉例
~~~~~

----

> 第一種
抄寫的行為只會讓你接收到文字本身而不是文字背後的意義


至於畫重點的更不用說


> 第二種類型的筆記

想像成你在對人解說一件事情
就算很口語也不會怎樣


> 第三種類型
feynman learning



----


 為什麼要寫筆記
首先大腦無法在同時間記住所有事情，筆記可以用來記錄下繁瑣的訊息

並且筆記

加以彙整



自學了兩年對寫筆記這件事小有心得，這篇文章就和各位分享一下我是如何做筆記的，以及筆記的類型


## 筆記的類型

根據自己過去做筆記的經驗，我將筆記區分為三種類型

開頭這邊先粗略介紹一下各種類型筆記的差異，以及它們的學習效益

### 純粹紀錄

**最簡單但是學習效益也最低的類型 - 純粹的紀錄**

將接收到的資訊或簡略的記錄下來，通常用於短期間內紀錄大量資訊，方便後續翻找


### 匯集和重整

**花費較多時間但可以加深對資訊理解程度的類型 - 匯集和重整接收到的資訊**

將繁瑣零碎的資訊用自己的解方式重新描述

### 理解和範例

**最困難但能真正理解(?)  - 舉出範例加以驗證**

釐清資訊間的因果邏輯，並舉出例子加以驗證



這三種筆記可以將他們視為獨立的類型，或是做為不同階段逐步修改


## 如何做筆記

上面的部分解說了筆記的類型以及學習效益

### 純粹紀錄

即使是記錄這也不等於要把一切資訊都寫下來，第一部分有說到他的目的只是"方便後續翻找"

只需要精簡的紀錄關鍵字，

表面意思





### 匯集和重整

### 理解和範例





# =====

## 純粹紀錄

第一種類型的筆記 - 純粹的紀錄，將接收到的一切全部(或簡略的)記錄下來，做起來最簡單但是學習效益也最低的類型，通常用於短期間內紀錄大量資訊，方便後續翻找

以剛接觸程式語言時為例，就算教學影片中有對每個關鍵字進行解釋，但要短時間內將他們全部記住也太強人所難了，這時我就會開個記事本將這些字詞寫下來，並且簡單描述它們的用途

這是我剛學 Hugo 時寫的筆記

```
> Folder
archetypes - 允許定義數據，像是語言設置
content - 儲存網站內容
data - 資料庫，可以讓網站提取資料
layouts - 定義布局
static - 儲存網站的所有靜態資料
thiemes - 主題、模板
config - 設置

> Contnet
前題 frontmatter (YAML, TOML, JSON)
可以有設置如語言的東西

> cmd
hugo server -D 顯示草稿文件
hugo new _index.md 索引文件，列表文件的內容
hugo new file.md 建立文件
```

就算只是簡單的記錄下來也可以幫助記憶，

這種類型的筆記也不太講求精確性，因為需要在遺忘時稍微翻閱而已



在學校上課時，無論是將課本的內容寫進筆記，或中將老師寫在黑板上的內容抄下來也屬於這種類型，即使老師寫的內容已經整理過了也一樣

問題在於單純的抄寫是幾乎沒有學習效益的，你接收到的只是單純文字

而不是文字裡的內容，要真正接收文字裡的資訊必須親自進行彙集和重整


## 匯集和重整
第二種類型的筆記 - 匯集和重整接收到的資訊

做起來會稍微辛苦點，但


什麼是重整? 就是將接收到的資訊用自己的理解方式重新解釋，當你嘗試解釋一件事時才會注意到有哪部分是過去不懂的
想像成在對別人解釋

有時會花一個下午整理一篇

一邊實作一邊寫筆記




## 理解和範例
第二種類型的筆記 - 理解資訊之間的因果邏輯，並且舉出範例加以驗證

筆記的最終階段，作起來相當困難但是能夠真正理解




參考資料
https://www.playpcesor.com/2016/08/tell-your-note-2.html
https://www.playpcesor.com/2016/11/4.html
https://www.playpcesor.com/2016/09/help-you-write-something.html







<!--more-->
