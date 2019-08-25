Vagrant.configure("2") do |config|
  config.vm.box = "ubuntu/bionic64"
  config.vm.provider "virtualbox"

  #config.vm.synced_folder '.', '/vagrant', disabled: true
  config.vm.network "private_network", ip: "192.168.33.11"

  config.vm.provider "virtualbox" do |vb|
     vb.gui = false
     vb.name = "mysql-box"
     vb.memory = "4096"
	 vb.cpus = 2
  end
  
  config.vm.provision "shell", privileged: true, path: "configs/install.sh"
  config.vm.provision "shell", privileged: true, path: "configs/start.sh", run: "always"
  
end
