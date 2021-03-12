
# İş ihtiyacımız
Bir bankada krediler bölümünde çalıştığımızı düşünelim.
İş geliştirme ekibi kredi başvuruların arttırılması, süreçlerinin daha hızlı ve efektif olarak yenilenmesi talep etmektedir.
Başvurulan krediler bir mülk için olması gerekmektedir. 
Süreç için, uygulama puanı hesaplamasını otomatikleştirmek ve çevrimiçi mevcut kaynaklardan müşteriler hakkındaki bilgileri birleştirmek istiyorlar.

Puanlama kurallarının eklenmesi ve değiştirilmesinin kolay olmasını istiyorlar.
Puanlama sonuçu olumsuz olan başvurular otomatik olarak reddedilmeli, 
diğerleri müşteri tarafından gönderilen belgelerin manuel olarak kontrol edilmesini gerektirecek ve ardından onay veya ret kararı kaydedilmelidir.

#### Kredi başvuru süreci adımları:
* Operatör; müşteri verilerini, mülk, kredi bilgileri ve müşteri tarafından sağlanan ekli belgeler ile kredi başvurusu yapar.  (Registeration)
* Sistem basit kontolleri gerçekletirir : gerekli alanlar, veri formatlarını
* Operatör; sisteme skor kurallara göre hesaplama işlemini verir.
* Skor red(kırmızı) ise başvuru reddedilir ve açıklama yapılır.
* Skor olumlu(yeşil) ise, operatör ekli belgeleri doğrular ve verilen veri ve belgeleri arasındaki tutarsızlıklar nedeniyle başvuruyu kabul eder veya reddeder. (ScoreResult)
* Her operatörün onaylayabileceği kredi tutar sınırlaması olmalı (CompetenceLevel), yetkisini  aşan kredi tutarlarını onaylayamamalı. (Decision)
* Onay/Red olmuş kredi başvuruları kredi@banka.com adresine otomatik bildirim yapılması gerekiyor.
* Kredilerin durumlarını özet olarak görmemiz gerekiyor.
#### Uygulama puanı hesaplamamız için puanlama kurallarını inceleyelim. İşte kurallar:

* Mülk değeri talep edilen kredi tutarını aşmamalıdır.
* Son kredi taksit günü, müşteri yaşı 65 geçmemelidir.
* Aylık kredi taksiti, müşterinin aylık gelirinin %15'ini geçmemelidir.
* Müşterinin Türkiye de herhangi kayıtlı bir borçu olmamalıdır. (Gelir İdare başkanlığı servisi kullanılacak)


## Domain Expert ile görüşme

<b>Developer</b> : Birinin yaşını hesaplarken sadece bir yılı mı yoksa tam doğum gününü mü düşünmeliyim?

<b>Expert</b> : Müşterinin yaşını hesaplarken doğum gününü düşünerek hesaplamalısın.

<b>Developer</b> :  Kredi için aylık taksiti nasıl hesaplayabilirim?

<b>Expert</b> : 
```
Aylık Ödeme = P (r(1+r)^n)/((1+r)^n-1)
```
r = Faiz Oranı, n = ödeme sayısı , P = anapara ; gördüğün gibi gayet basit.
  
<b>Developer</b> :  Para birimi veya kredi tutarı her zaman müşterinin geliriyle aynı para biriminde mi? 

<b>Expert</b> : Evet, sadece TL den bahsediyoruz.

<b>Developer</b> :  Son taksit tarihini hesaplarken başlangıç ​​tarihi olarak hangi tarihi almalıyım?

<b>Expert</b> :  Şimdilik başlangıç tarihini başvuru kabul tarihi olarak kabul edelim. Fakat daha sonra farklı bir yöntem izleyebiliriz.
Geliştirmeni bunu göze alarak yapmanda fayda var.


### links
https://www.youtube.com/watch?v=Z62cbp61Bb8

https://www.youtube.com/watch?v=LDRxo6wDIE0