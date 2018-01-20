# RPI SETUP

## Temperature Sensor DS18B20 1-wire

This setup considers RPI attached to the following hardware sensor:

![DS18B20](https://i.imgur.com/MgeMeal.png)

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

Starting from LIRC Version 0.9.4+, the Debian distribution comes with a default configuration based on the `devinput` driver. This scheme should work out of the box with the following limitations:

- There must be exactly one capture device supported by the kernel.
- The remote(s) used must be supported by the kernel.
- There is no need to do IR blasting (i. e., to send IR data).

Linux Raspbian 4.9.54-v7+ supports several IR drive options. First option is the best  if you just need IR input for remote control because `devinput` drive is already set. There is a second option to shutdown the RPI through a GPIO pin. But if IR output is needed then you should follow option 3, in order to fully install and configure the *Linux Infrared Remote Control for Raspberry Pi*. 

### Option 1: gpio-ir

- gpio-ir uses the upstream Linux gpio_rc_recv driver and supports in-kernel decoding (aka ir-keytable configuration)
- gpio-r uses kernel embedded IR software and keys directly to /dev/input/event* device
- all decoding is done by the kernel and key mapping is configured by ir-keytable tool
- Lirc installation is not required
- GPIO pin 18 default
- The option `driver = devinput` should be configured
- Only for IR input. If IR output blast is required then use option 3 

Add to /boot/config.txt

    dtoverlay=gpio-ir,gpio_pin=18,gpio_pull=down,rc-map-name=rc-rc6-mce

### Option 2: gpio-shutdown

- Initiates a shutdown when GPIO changes

Add to /boot/config.txt

    dtoverlay=gpio-shutdown,gpio_pin=3,active_low=1,gpio_pull=up

### Option 3: lirc-rpi

This setup installs Lirc with both IR input and output, the prototype below also shows the temperature sensor.

![prototype-2](https://i.imgur.com/Ops6Wit.png)

#### IR input

- Keyes KY-022 with IR sensor receiver 838B, connected at GPIO18 (BCM 12).

![](https://i.imgur.com/MKWqk60.png)

#### IR output

Infrared led driven by a 2N3904 transistor, connected at GPIO17 (BCM 11).

![IR output](https://i.imgur.com/cbiJUpb.png)


## LIRC: Linux Infrared Remote Control for Raspberry Pi



#### Lirc 0.9.4 disruptive update

The Linux Infrared Remote Control for Raspberry Pi is derived from the original Lirc serial driver by [Aron Szabo](http://aron.ws/projects/lirc_rpi/ "original lirc for rpi"). Also a further development by Bengt Martensson [improved the Lirc driver](https://github.com/bengtmartensson/lirc_rpi "lirc_rpi"). The consequence is that Lirc configuration [changed so much](https://www.raspberrypi.org/forums/viewtopic.php?f=28&t=192891#p1212574) that updating from 0.9.0 requires special intervention. This can be done using an update script or a manual process. Most users just need some manual steps but a tricky script is also available to "convert" the old configuration, it can be found at '/usr/share/lirc/lirc-old2new.sh'.

The good news is that if you are installing Lirc for the first time, the current Raspberry Pi / Raspbian Stretch comes with Lircd version 0.9.4c that applies several improvements to make your life much better. Files required at previous version are not used anymore: `/etc/modules` and `/etc/lirc/hardware.conf`.  

If you need more info, look for Device Tree at RPI website. Below, links and definitions that may help and  the `lirc-rpi` section extracted from official documents, showing parameters details.

- [Overlay and Parameter Reference](https://github.com/raspberrypi/firmware/blob/master/boot/overlays/README "Lircd Parameters") and 
- [DEVICE TREES, OVERLAYS, AND PARAMETERS](https://www.raspberrypi.org/documentation/configuration/device-tree.md#part3).

> "With a Device Tree, the kernel will automatically search for and load modules that support the indicated enabled devices. As a result, by creating an appropriate DT overlay for a device you save users of the device from having to edit  /etc/modules; All of the configuration goes in config.txt, and in the case of a HAT, even that step is unnecessary. Note, however, that layered modules such as  i2c-dev still need to be loaded explicitly."

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

### Lirc setup

In order to update and upgrade Raspbian and install Lirc at RPI, run the command:

	  apt-get update \
	  && apt-get upgrade -y \
	  && apt-get install -y lirc \
	  && rm -rf /var/lib/apt/lists/*

### Config.txt

The Lirc version 0.9.4c is configured ONLY by the file `/etc/config.txt`.

Add to /boot/config.txt

    # Uncomment this to enable the lirc-rpi module
    dtoverlay=lirc-rpi,gpio_out_pin=17,gpio_in_pin=18,gpio_in_pull=up

### Change to driver default

Edit file /etc/lirc/lirc_options.conf and change:

    from:
    driver  = devinput
    device  = auto
    
    to:
    driver  = default
    device  = /dev/lirc0
    
## Reboot and check

#### lircd status

	root@a4cfc1934fd1:/app# /etc/init.d/lircd status
	[ ok ] lircd is running.
	
	root@d06ba23ebb3d:/etc# /etc/init.d/lircd --help
	Usage: /etc/init.d/lircd {start|stop|reload|restart|force-reload|status}

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

Press remote control keys near the IR receiver e check pulse/space info at screen:

    space 28794
    pulse 80
    space 19395
    pulse 83
    space 402351
    pulse 135
    space 7085
    pulse 85
    space 2903

#### irw

irw reads data from a socket created by lircd. To get irw working you need to start lircd:

- lircd reads raw data from /dev/lirc0
- decodes it and makes it available on /var/run/lirc/lircd
- this is where irw expects decoded data

However, for this to work you need to configure lirc_options.conf with driver and device and also add the proper config file for your remote to /etc/lirc/lircd.conf.d

#### /etc/lirc/lircd.conf.d

- [lirc24.conf](https://github.com/josemotta/Api/blob/master/gpio-base/lirc24.conf "lirc24"): 24 Key LED Controller
- [lirc44.conf](https://github.com/josemotta/Api/blob/master/gpio-base/lirc44.conf "lirc44"): 44 Key LED Controller

## Capturing IR from remotes

Following CNXSoft article [How to Control Your Air Conditioner with Raspberry Pi Board and ANAVI Infrared pHAT](https://www.cnx-software.com/2017/03/12/how-to-control-your-air-conditioner-with-raspberry-pi-board-and-anavi-infrared-phat/) the IR input maybe captured from any air conditioner, for example, split model Comfee with remote control ref. RG06A6/BGE.

More info:

- [ANAVI Infrared pHAT](https://github.com/AnaviTechnology/anavi-docs/blob/master/anavi-infrared-phat/anavi-infrared-phat.md "ANAVI Infrared pHAT") website.
- Leon Anavi template for [lircd-acpanasonic.conf](https://gist.github.com/leon-anavi/6b7d6c2daaefef5b01054a8b8a0397ae "lircd-acpanasonic.conf").
- General remote capture at [KODI HOW-TO:Set up LIRC](http://kodi.wiki/view/HOW-TO:Set_up_LIRC "KODI")



#### /etc/lirc/lircd.conf.d/lircd-air.conf

TODO: commands below maybe captured and manually edited to the configuration file. These commands, together with temperature sensor, should be used for automatic temperature control inside the environment.

- low fan
- high fan
- lowest temperature (17)
- best temperature (22)
- off

#### irdb-get

As reported by [ArchLinux Lirc](https://wiki.archlinux.org/index.php/LIRC "ArchLinux Lirc"), apps are able to search the [remotes database](http://lirc-remotes.sourceforge.net/remotes-table.html "DB") in order to obtain the definition of scancodes to keymaps required to allow LIRC to manage a remote.

	$ irdb-get find stream
	atiusb/atiusb.lircd.conf
	digital_stream/DTX9900.lircd.conf
	snapstream/Firefly-Mini.lircd.conf
	streamzap/PC_Remote.lircd.conf
	streamzap/streamzap.lircd.conf
	x10/atiusb.lircd.conf
	
	$ irdb-get download streamzap/streamzap.lircd.conf 
	Downloaded sourceforge.net/p/lirc-remotes/code/ci/master/tree/remotes/streamzap/streamzap.lircd.conf
	as streamzap.lircd.conf

Once identified, copy the needed conf to /etc/lirc/lircd.conf.d/ to allow the daemon to initialize support for it.

	# cp streamzap.lircd.conf /etc/lirc/lircd.conf.d/

## Testing IR output

#### Checking available devices

	pi@lumi:~ $ irsend list "" ""

	LED_24_KEY
	LED_44_KEY
	pi@lumi:~ $ irsend list LED_24_KEY ""

	0000000000000001 BRIGHT_DOWN
	0000000000000002 BRIGHT_UP
	0000000000000003 OFF
	0000000000000004 ON
	0000000000000005 RED
	0000000000000006 GREEN
	0000000000000007 BLUE
	0000000000000008 WHITE
	0000000000000009 ORANGE
	000000000000000a PEA_GREEN
	000000000000000b DARK_BLUE
	000000000000000c 7_JUMP
	000000000000000d DARK_YELLOW
	000000000000000e CYAN
	000000000000000f BROWN
	0000000000000010 ALL_FADE
	0000000000000011 YELLOW
	0000000000000012 LIGHT_BLUE
	0000000000000013 PINK
	0000000000000014 7_FADE
	0000000000000015 STRAW_YELLOW
	0000000000000016 SKY_BLUE
	0000000000000017 PURPLE
	0000000000000018 3_JUMP
	pi@lumi:~ $

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

## Docker tips

#### Bash or ssh into a running container in background mode
	docker exec -it <containerIdOrName> bash
	docker exec -it 665b4a1e17b6 /bin/bash #by ID
	docker exec -i -t loving_heisenberg /bin/bash #by Name
[See more at this link](https://askubuntu.com/questions/505506/how-to-get-bash-or-ssh-into-a-running-container-in-background-mode)

#### Remove dangling Docker images
	docker system prune
    docker container prune
    docker image prune

	Linux cron utility:

	crontab -e
	#add this line to the bottom and save it:
	0 3 * * * /usr/bin/docker system prune -f

[See more at this link](https://nickjanetakis.com/blog/docker-tip-32-automatically-clean-up-after-docker-daily)

#### Util
    ip a
    docker network ls
    docker network inspect host

#### Nuke Docker images

	#!/bin/bash
	
	# Stop all containers
	docker stop $(docker ps -a -q)
	
	# Delete all containers
	# https://techoverflow.net/blog/2013/10/22/docker-remove-all-images-and-containers/
	# https://github.com/stuckless/sagetv-dockers/blob/master/nukeAllDockers.sh
	docker rm $(docker ps -a -q)

	# Delete all images except base: gpio | home-web
	# docker rmi --force $(docker images -q)
	docker images -q | grep -v "`docker images | grep 'gpio\|home-web' | awk -F \" \" '{print $3}'`"
    
#### Remove dangling images

    docker images -f dangling=true
    docker rmi $(docker images -f dangling=true -q)

#### Net status

    netstat -l	listening
    netstat -lt	listening TCP
    netstat -at	TCP
    netstat -au	UDP
    netstat -st	statistics TCP
    netstat -ie	ifconfig
    netstat -g	multicast group
    netstat -r	routing
    netstat -tp	service name

