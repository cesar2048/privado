clear ; close all;

function [ ] = fillData ( fileIn )
	%% =========== Pitch :: Fill unvoiced frames with ceros =============
	fprintf('>> Extracting data from \"%s\" ...\n', fileIn);
	sampleRate	= 44100;
	frameOfset	= 256;
	% datosR	= csvread(strcat(fileIn,".csv"));
	datosR		= csvread(fileIn);
	muestras	= size(datosR,1);
	frames		= zeros(muestras,1);

	for cont=1:muestras
		frames(cont,1)=((sampleRate*datosR(cont,1))/frameOfset);
	end
	ultimoFrame=frames(muestras);
	datosR=[datosR frames];
	resultados=zeros(ultimoFrame,3);
	frameMatriz2=0;
	contadorMatriz2=1;
	for cont2=1:ultimoFrame
		frameMatriz2=datosR(contadorMatriz2,3);
		if(cont2<frameMatriz2)
			resultados(cont2,1)= ((frameOfset*cont2)/sampleRate);
			resultados(cont2,2)=0;
			resultados(cont2,3)=cont2;
		else
			resultados(cont2,:)=datosR(contadorMatriz2,:);
			contadorMatriz2=contadorMatriz2+1;
		endif
	end
	res2=resultados(:,1:2);
	% csvwrite (strcat(fileIn,"_ceros.csv"), res2);
	csvwrite( fileIn, res2 );
end


% main
if nargin > 0
	arg_list 	= argv ();
	fileIn		= arg_list{1};

	fid = fopen(fileIn, "r");
	if fid ~= -1
		fclose(fid);
		fillData(fileIn);
	end
end