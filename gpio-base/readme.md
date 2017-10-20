## RPI INFRARED SETUP

## Docker

#### P&D Windows 10 x64

    docker container prune
    docker image prune
    docker container ls -a
    docker-compose -f docker-compose.ci.build.yml build
    docker push josemottalopes/io.swagger:latest

#### Run Raspberry Pi arm

    docker run --privileged -d -p 5000:5000 josemottalopes/io.swagger:latest

#### Test

    http://lumi:5000/swagger/ui/index.html

#### Util
    ip a
    docker network ls
    docker network inspect host
    
#### Remove dangling images

    docker images -f dangling=true
    docker rmi $(docker images -f dangling=true -q)

    netstat -l	listening
    netstat -lt	listening TCP
    netstat -at	TCP
    netstat -au	UDP
    netstat -st	statistics TCP
    netstat -ie	ifconfig
    netstat -g	multicast group
    netstat -r	routing
    netstat -tp	service name

## 1-wire + Temp Sensor DS18B20

### 1-Wire 

Append to /boot/config.txt (no spaces) and reboot:

    dtoverlay=w1-gpio,gpiopin=4,pullup=on    
    
    dtoverlay=w1-gpio-pullup,gpiopin=4

GPIO 4 of RPi header pin 7

#### List

    lsmod | grep w1

#### Start

    modprobe w1_gpio
    modprobe w1_therm

#### Read temperature

    cd /sys/bus/w1/devices
    cd 28-*
    cat w1_slave

## Infrared + Keyes IR input 838B

### opção1: gpio-ir

- uses kernel embedded IR software
- receive keys directly to /dev/input/event* device
- all decoding is done by the kernel
- key mapping configured by ir-keytable tool
- Lirc is not required
- GPIO pin 18 default

Add to /boot/config.txt

    dtoverlay=gpio-ir, gpio_pin=18,gpio_pull=down,rc-map-name=rc-rc6-mce

### opção2: gpio-shutdown

- Initiates a shutdown when GPIO changes

Add to /boot/config.txt

    dtoverlay=gpio-shutdown,gpio_pin=3,active_low=1,gpio_pull=up

### opção3: lirc-rpi

- LIRC: Linux Infrared Remote Control for Raspberry Pi
- Bengt Martensson has a further development of [the improved Lirc driver](https://github.com/bengtmartensson/lirc_rpi "lirc_rpi").
- This seems to be the latest version available at Raspbian 4.9.54-v7+ distro and replaced the [original from Aron Szabo](http://aron.ws/projects/lirc_rpi/ "original lirc for rpi"), which in turn was derived from the Lirc serial driver.
- Old configuration file `/etc/lirc/hardware.conf` should be converted
- New configuration format at `/etc/lirc/lirc_options.conf`

#### Add to /boot/config.txt (all default settings here)

    dtoverlay=lirc-rpi,gpio_out_pin=17,gpio_in_pin=18,gpio_in_pull=down

- sense: 0-active high, 1 active low, -1 auto detection default
- softcarrier: default on
- invert: signal at output pin, default off 
- debug: add additional messages, default off

#### Install Lirc

    sudo apt-get install lirc

#### Add to /boot/config.txt (true setup here)

    # Uncomment this to enable the lirc-rpi module
    dtoverlay=lirc-rpi,gpio_out_pin=16,gpio_in_pin=17,gpio_in_pull=up

    or

    dtoverlay=lirc-rpi
    dtparam=gpio_out_pin=16
    dtparam=gpio_in_pin=17
    dtparam=gpio_in_pull=up

#### Add to /etc/modules

    lirc_dev
    lirc_rpi 

    # tirei fora porque estava dando erro?
    #lirc_rpi gpio_in_pin=17 gpio_out_pin=16 gpio_in_pull=up

#### Change /etc/lirc/lirc_options.conf

    from:
    driver  = devinput
    device  = auto
    
    to:
    driver  = default
    device  = /dev/lirc0

#### Deprecated? previous version? /etc/lirc/hardware.conf

    LIRCD_ARGS="--uinput"
    LOAD_MODULES=true
    DRIVER="default"
    DEVICE="/dev/lirc0"
    MODULES="lirc_rpi"
    LIRCD_CONF=""
    LIRCMD_CONF=""

## Reboot

#### Check System 

    systemctl stop lircd
    systemctl start lircd
    systemctl status lircd

#### checking GPIO ports

    lsmod | grep lirc

    cat /sys/kernel/debug/gpio    
    GPIOs 0-53, bcm2708_gpio:
     gpio-16  (led0) out hi
     gpio-17  (lirc_rpi ir/out ) in  lo
     gpio-18  (lirc_rpi ir/in  ) in  lo

    mode2 --driver default --list-devices
    /dev/lirc0

    cat /proc/bus/input/devices

    journalctl -x

    dmesg | grep lirc 



#### Tests with mode2

    mode2 --driver default --device /dev/lirc0

#### Testing the IR receiver

Run these two commands to stop lircd and start outputting raw data from the IR receiver

    sudo /etc/init.d/lirc stop
    mode2 -d /dev/lirc0

acionar controle remoto perto IR receiver e constatar na tela pulse/space:

    space 28794
    pulse 80
    space 19395
    pulse 83
    space 402351
    pulse 135
    space 7085
    pulse 85
    space 2903

For starting TV and then MUTE I created a small script:

    #!/bin/bash
    /usr/bin/irsend SEND_ONCE lg_tv KEY_POWER
    sleep 10
    /usr/bin/irsend SEND_ONCE lg_tv KEY_MUTE

