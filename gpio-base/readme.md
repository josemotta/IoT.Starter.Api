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

This setup considers following hardware:

- [Dallas DS18B20](https://cdn.sparkfun.com/datasheets/Sensors/Temp/DS18B20.pdf "DS18B20") Programmable Resolution 1-Wire Digital Thermometer
- Keyes assembly includes pull up and led

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

- gpio-ir uses the upstream Linux gpio_rc_recv driver and supports in-kernel decoding (aka ir-keytable configuration)
- gpio-r uses kernel embedded IR software and keys directly to /dev/input/event* device
- all decoding is done by the kernel and key mapping is configured by ir-keytable tool
- Lirc is not required
- GPIO pin 18 default
- The option `driver = devinput` should be configured

Add to /boot/config.txt

    dtoverlay=gpio-ir,gpio_pin=18,gpio_pull=down,rc-map-name=rc-rc6-mce

### opção2: gpio-shutdown

- Initiates a shutdown when GPIO changes

Add to /boot/config.txt

    dtoverlay=gpio-shutdown,gpio_pin=3,active_low=1,gpio_pull=up

### opção3: lirc-rpi

#### LIRC: Linux Infrared Remote Control for Raspberry Pi

Bengt Martensson has a further development of a [improved Lirc driver](https://github.com/bengtmartensson/lirc_rpi "lirc_rpi") to replace the [original from Aron Szabo](http://aron.ws/projects/lirc_rpi/ "original lirc for rpi"), which in turn was derived from the Lirc serial driver. I figured out that Raspberry Raspbian Stretch version 0.9.4c applies these changes. 

- Old configuration file `/etc/lirc/hardware.conf` should be converted
- New configuration format at `/etc/lirc/lirc_options.conf`
- Add custom devices to /etc/lirc/lircd.conf.d/
- IR output requires that configuration changes `driver = default` 

#### Lircd setup for Raspberry Pi 2 model B

    sudo apt-get install lirc

This setup installed Lircd version 0.9.4c, with following hardware:

- IR input: Keyes IR input 838B connected at GPIO18 (BCM 12)
- IR output: a IR led driven by a 2N3904 transistor connected at GPIO17 (BCM 11)

#### Add to /boot/config.txt

    # Uncomment this to enable the lirc-rpi module
    dtoverlay=lirc-rpi,gpio_out_pin=17,gpio_in_pin=18,gpio_in_pull=up

##### Lircd version 0.9.4c

The parameters are configured ONLY in `/etc/config.txt`.

[DEVICE TREES, OVERLAYS, AND PARAMETERS](https://www.raspberrypi.org/documentation/configuration/device-tree.md#part3) states that "with a Device Tree, the kernel will automatically search for and load modules that support the indicated enabled devices. As a result, by creating an appropriate DT overlay for a device you save users of the device from having to edit  /etc/modules; all of the configuration goes in config.txt, and in the case of a HAT, even that step is unnecessary. Note, however, that layered modules such as  i2c-dev still need to be loaded explicitly."

Please see details below, extracted from [The Overlay and Parameter Reference](https://github.com/raspberrypi/firmware/blob/master/boot/overlays/README "Lircd Parameters"):

    Name:   lirc-rpi
	Info:   Configures lirc-rpi (Linux Infrared Remote Control for Raspberry Pi)
        	Consult the module documentation for more details.
	Load:   dtoverlay=lirc-rpi,<param>=<val>
	Params: gpio_out_pin        GPIO for output (default "17")

        	gpio_in_pin         GPIO for input (default "18")

        	gpio_in_pull        Pull up/down/off on the input pin
                                (default "down")

        	sense               Override the IR receive auto-detection logic:
                                 "0" = force active-high
                                 "1" = force active-low
                                 "-1" = use auto-detection
                                (default "-1")

        	softcarrier         Turn the software carrier "on" or "off"
                                (default "on")

        	invert              "on" = invert the output pin (default "off")

        	debug               "on" = enable additional debug messages
                                (default "off")
  

#### Change to driver default in /etc/lirc/lirc_options.conf

    from:
    driver  = devinput
    device  = auto
    
    to:
    driver  = default
    device  = /dev/lirc0

#### /etc/modules

Do not need changes. Previous version (no device-tree) required following lines: 

    lirc_dev
    lirc_rpi 

	#lirc_rpi gpio_in_pin=18 gpio_out_pin=17 gpio_in_pull=up

#### /etc/lirc/hardware.conf is deprecated

    LIRCD_ARGS="--uinput"
    LOAD_MODULES=true
    DRIVER="default"
    DEVICE="/dev/lirc0"
    MODULES="lirc_rpi"
    LIRCD_CONF=""
    LIRCMD_CONF=""

## Reboot and check

#### lircd.socket

	root@lumi:~# systemctl status lircd.socket
	â lircd.socket
	   Loaded: loaded (/lib/systemd/system/lircd.socket; enabled; vendor preset: enabled)
	   Active: active (running) since Tue 2017-10-24 22:59:34 -02; 1 day 20h ago
	   Listen: /run/lirc/lircd (Stream)
	
	Oct 24 22:59:34 lumi systemd[1]: Listening on lircd.socket.

#### lircd.service  

	root@lumi:~# systemctl status lircd.service
	â lircd.service - Flexible IR remote input/output application support
	   Loaded: loaded (/lib/systemd/system/lircd.service; enabled; vendor preset: enabled)
	   Active: active (running) since Tue 2017-10-24 22:59:41 -02; 1 day 20h ago
	     Docs: man:lircd(8)
	           http://lirc.org/html/configure.html
	 Main PID: 422 (lircd)
	      CPU: 7.314s
	   CGroup: /system.slice/lircd.service
	           ââ422 /usr/sbin/lircd --nodaemon
	
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: Using remote: LED_44_KEY.
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Notice: lircd(default) ready, using /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: lircd(default) ready, using /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Info: Cannot configure the rc device for /dev/lirc0
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: Cannot configure the rc device for /dev/lirc0
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd

#### checking GPIO ports - vcdbg

    root@lumi:~# vcdbg log msg |& grep -v -E "(HDMI|gpioman|clock|brfs)"
	001307.388: *** Restart logging
	001309.444: Read command line from file 'cmdline.txt'
	dwc_otg.lpm_enable=0 console=serial0,115200 console=tty1 root=PARTUUID=fe04de35-02 rootfstype=ext4 elevator=deadline fsck.repair=yes rootwait
	001642.582: Loading 'kernel7.img' to 0x8000 size 0x45f7b8
	001646.210: No kernel trailer - assuming DT-capable
	001649.832: Loading 'bcm2709-rpi-2-b.dtb' to 0x4677b8 size 0x4135
	001785.536: Loaded overlay 'lirc-rpi'
	001785.554: dtparam: gpio_out_pin=17
	001786.070: dtparam: gpio_in_pin=18
	001786.586: dtparam: gpio_in_pull=up
	001787.202: dtparam: audio=on
	001863.296: Loaded overlay 'w1-gpio-pullup'
	001863.312: dtparam: gpiopin=4
	003032.644: Device tree loaded to 0x2effb800 (size 0x47ed)
	004537.231: vchiq_core: vchiq_init_state: slot_zero = 0xfad80000, is_master = 1
	004548.430: TV service:host side not connected, dropping notification 0x00000002, 0x00000002, 0x00000044
	385090.775: TV service:host side not connected, dropping notification 0x00000001, 0x00000002, 0x00000000
	386518.244: TV service:host side not connected, dropping notification 0x00000002, 0x00000002, 0x00000044

#### lsmod
    lsmod | grep lirc
	lirc_rpi                9032  0
	lirc_dev               10583  1 lirc_rpi
	rc_core                24377  1 lirc_dev

#### gpio
    cat /sys/kernel/debug/gpio    
	gpiochip0: GPIOs 0-53, parent: platform/3f200000.gpio, pinctrl-bcm2835:
	 gpio-4   (                    |w1                  ) in  hi
	 gpio-5   (                    |w1 pullup           ) out hi
	 gpio-35  (                    |?                   ) in  hi
	 gpio-47  (                    |?                   ) out lo
#### devices
    mode2 --driver default --list-devices
    /dev/lirc0

    cat /proc/bus/input/devices
	I: Bus=0003 Vendor=045e Product=07f8 Version=0111
	N: Name="Microsoft Wired Keyboard 600"
	P: Phys=usb-3f980000.usb-1.4/input1
	S: Sysfs=/devices/platform/soc/3f980000.usb/usb1/1-1/1-1.4/1-1.4:1.1/0003:045E:07F8.0002/input/input1
	U: Uniq=
	H: Handlers=kbd event1
	B: PROP=0
	B: EV=1f
	B: KEY=3007f 0 0 0 0 483ffff 17aff32d bf544446 0 0 1 130c13 b17c000 267bfa d941dfed 9e1680 4400 0 10000002
	B: REL=40
	B: ABS=1 0
	B: MSC=10

#### journalctl

	root@lumi:~# journalctl -b 0 /usr/sbin/lircd
	-- Logs begin at Tue 2017-10-24 22:59:32 -02, end at Thu 2017-10-26 19:27:50 -02. --
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: lircd:  Opening log, level: Info
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: Initial device: /dev/lirc0
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: Initial device: /dev/lirc0
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Info: lircd:  Opening log, level: Info
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: lircd:  Opening log, level: Info
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Notice: Using systemd fd
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Warning: Running as root
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: Using systemd fd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Warning: Running as root
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Info: Using remote: LED_24_KEY.
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: Using remote: LED_24_KEY.
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Info: Using remote: LED_44_KEY.
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: Using remote: LED_44_KEY.
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Notice: lircd(default) ready, using /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: lircd(default) ready, using /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Info: Cannot configure the rc device for /dev/lirc0
	Oct 24 22:59:41 lumi lircd[422]: lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Info: Cannot configure the rc device for /dev/lirc0
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd
	Oct 24 22:59:41 lumi lircd-0.9.4c[422]: Notice: accepted new client on /var/run/lirc/lircd

#### dmesg | grep lirc 

	root@lumi:~# dmesg | grep lirc
	[    5.839181] lirc_dev: IR Remote Control driver registered, major 243
	[    5.845182] lirc_rpi: module is from the staging directory, the quality is unknown, you have been warned.
	[    6.936736] lirc_rpi: auto-detected active low receiver on GPIO pin 18
	[    6.947172] lirc_rpi lirc_rpi: lirc_dev: driver lirc_rpi registered at minor = 0
	[    6.947186] lirc_rpi: driver registered!
	[   13.863775] input: lircd-uinput as /devices/virtual/input/input2
	[46081.760131] lirc_rpi: AIEEEE: 1 1 59f095fa 59f095dc 85350 be7dd
	[46102.560135] lirc_rpi: AIEEEE: 0 0 59f0960f 59f095ff 54590 93bc9
	[60477.200408] lirc_rpi: AIEEEE: 1 1 59f0ce35 59f0ce00 db1dc 23902
	[60567.675029] lirc_rpi: AIEEEE: 0 0 59f0ce90 59f0ce47 5abef 235bdd

#### mode2 devices

    mode2 --driver default --list-devices
	/dev/lirc0

## Testing IR input

    systemctl stop lircd.socket
	systemctl stop lircd.service

    mode2 --driver default --device /dev/lirc0

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

## Testing IR output

#### irw

irw reads data from a socket created by lircd. To get irw working you need to start lircd:

- lircd reads raw data from /dev/lirc0
- decodes it and makes it available on /var/run/lirc/lircd
- this is where irw expects decoded data

However, for this to work you need to configure lirc_options.conf with driver and device and also add the proper config file for your remote to /etc/lirc/lircd.conf.d

#### /etc/lirc/lircd.conf.d

- [lirc24.conf](https://github.com/josemotta/Api/blob/master/gpio-base/lirc24.conf "lirc24"): 24 Key LED Controller
- [lirc44.conf](https://github.com/josemotta/Api/blob/master/gpio-base/lirc44.conf "lirc44"): 44 Key LED Controller

#### Commanding lights

	# turn on
	pi@lumi:~ $ irsend SEND_ONCE LED_44_KEY POWER
	#change color
	pi@lumi:~ $ irsend SEND_ONCE LED_44_KEY WHITE
	pi@lumi:~ $ irsend SEND_ONCE LED_44_KEY CYAN
	pi@lumi:~ $ irsend SEND_ONCE LED_44_KEY WHITE
	# lights up and down
	pi@lumi:~ $ irsend --count=10 SEND_ONCE LED_44_KEY BRIGHT_UP
	pi@lumi:~ $ irsend --count=10 SEND_ONCE LED_44_KEY BRIGHT_UP
	pi@lumi:~ $ irsend --count=20 SEND_ONCE LED_44_KEY BRIGHT_DOWN
	pi@lumi:~ $ irsend --count=10 SEND_ONCE LED_44_KEY BRIGHT_UP

	# turn off
	pi@lumi:~ $ irsend SEND_ONCE LED_44_KEY POWER

#### A script for starting TV and then mute

    #!/bin/bash
    /usr/bin/irsend SEND_ONCE lg_tv KEY_POWER
    sleep 10
    /usr/bin/irsend SEND_ONCE lg_tv KEY_MUTE

