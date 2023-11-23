#!/bin/bash

echo "Pilih opsi:"
echo "1. Update library"
echo "2. Copy dll front"
echo "3. Copy dll back"
echo "4. Copy menu"
echo "5. Copy asset"
read -p "Masukkan pilihan Anda (1-5): " pilihan

case $pilihan in
  1)
    echo "Updating library..."
    # Tambahkan perintah Anda untuk memperbarui library di sini
    ;;
  2)
    echo "Copying dll front..."
    # Tambahkan perintah Anda untuk menyalin dll front di sini
    ;;
  3)
    echo "Copying dll back..."
    # Tambahkan perintah Anda untuk menyalin dll back di sini
    ;;
  4)
    echo "Copying menu..."
    # Tambahkan perintah Anda untuk menyalin menu di sini
    ;;
  5)
    echo "Copying asset..."
    # Tambahkan perintah Anda untuk menyalin asset di sini
    ;;
  *)
    echo "Pilihan tidak valid"
    ;;
esac