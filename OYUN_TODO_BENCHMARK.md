# Bloom & Blade — Todo / Benchmark Takibi

Durum işaretleri: `⬜ Bekliyor` `🔄 Yapılıyor` `✅ Bitti`

## Ana Yol Haritası

- [x] **Aşama 1 (✅)** Oynanabilir prototip  
  Benchmark: Player 8 yön hareket + SpringState saldırı + 1 WeedEnemy kovalamaca/hasar + tek sahnede 5 dk oynanış.
- [x] **Aşama 2 (✅)** Mevsim geçiş sistemi  
  Benchmark: Bahar/Yaz/Sonbahar/Kış anlık geçiş + her state farklı saldırı + temel cooldown/energy.
- [ ] **Aşama 3 (🔄)** Roguelike core loop  
  Benchmark: Oda temizle->ödül + en az 3 boon + ölüm->hub dönüş.
- [ ] **Aşama 4 (⬜)** Prosedürel oda üretimi  
  Benchmark: Grid tabanlı üretim + geçerli spawn/path + 20+ layout varyasyonu.
- [ ] **Aşama 5 (⬜)** Hub ve kalıcı ilerleme  
  Benchmark: kalıcı kaynak kaydı + görsel hub onarımı + en az 2 kalıcı upgrade.
- [ ] **Aşama 6 (⬜)** İçerik ve denge  
  Benchmark: 3+ düşman tipi + 12+ boon + temel zorluk eğrisi.
- [ ] **Aşama 7 (⬜)** Cila ve vitrin  
  Benchmark: temel VFX/SFX + okunaklı UI + paylaşılabilir demo çıktısı.

---

## Adım Adım Çalışma Planı

> Kural: **Şu an Adım 3 aktif. Adım 3 bitince Adım 4'e geçilecek.**

### Adım 1 — Prototip (Tamamlandı ✅)

- [x] Unity'de test sahnesi oluştur (zemin + Player + 1 WeedEnemy)
- [x] `PlayerController`: 8 yön hareket
- [x] `SpringState`: tek temel saldırı
- [x] `WeedEnemy`: oyuncuyu takip + temas hasarı
- [x] 5 dakika kesintisiz oynanış kontrolü

**Adım 1 bitiş kriteri:** Bu 5 kutunun tamamı tiklenmiş olmalı.

### Adım 2 — Mevsim Sistemi (Tamamlandı ✅)

- [x] `ISeasonState` arayüzü oluştur
- [x] `SpringState`, `SummerState`, `AutumnState`, `WinterState` sınıfları
- [x] Savaşta anlık state geçişi (tuşlarla)
- [x] Her state için farklı saldırı davranışı
- [x] Energy/Cooldown temel dengesi

### Adım 3 — Roguelike Core Loop (Şu an bunu yapıyoruz 🔄)

- [x] Oda temizlenince ödül seçim ekranı aç
- [x] En az 3 boon (ör: +hasar, +hız, +can)
- [ ] Oyuncu ölünce hub sahnesine dön
- [ ] Hub'dan run başlatma butonu
- [ ] Tek run akışını baştan sona oynanabilir yap

