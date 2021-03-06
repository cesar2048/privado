%% Machine Learning System

%%
%% Determine if a file exists
%%
function exists = fileExists( filename )
	fid = fopen(filename, "r");
	if fid ~= -1
		fclose(fid);
		exists = 1;
	else 
		exists = 0;
	end
end


%
% Splits the data matrix into two matrices, one for training and the other for testing
% 	The rows are randomly selected, the proportion indicates how many rows send for training
%
function [train test] = splitSamples( data, proportion, selector )
	
	[m,n] 		= size(data);
	
	% if selector is an empty matrix, generate one
	if ( sum(size(ones(0))) == 0)
		do
			selector = rand(m,1) <= proportion;
		until ( sum(selector) == round(m * proportion) )
	end
	
	train 		= zeros(m * proportion + 1	, n);
	test		= zeros(m * (1-proportion) 	, n);
	train_n		= 1;
	test_n		= 1;
	
	for i = [1:m]
		sel = selector(i);
		if sel
			train(train_n,:) = data(i,:);
			train_n ++;
		else
			test(test_n,:) = data(i,:);
			test_n++;
		end
	end
	
	csvwrite("temp/selector.csv", selector);
end

%%
%% Train neural network
%%
function [ Theta1 Theta2 ] = trainNerualNetwork( X, y, lambda, max_iter )

	input_layer_size  	= size(X,2);	% Pitch + MFCC data
	hidden_layer_size 	= 20;			% 20 hidden units
	m 					= size(X, 1);
	num_labels			= max(y);		% 
	
	% theta initialization
	initial_Theta1 = randInitializeWeights(input_layer_size, hidden_layer_size);
	initial_Theta2 = randInitializeWeights(hidden_layer_size, num_labels);
	initial_nn_params = [initial_Theta1(:) ; initial_Theta2(:)];	% unrolling

	fprintf('Training Neural Network... \n')
	options = optimset('MaxIter', max_iter);

	% Create "short hand" for the cost function to be minimized
	costFunction = @(p) nnCostFunction(p, ...
									   input_layer_size, ...
									   hidden_layer_size, ...
									   num_labels, X, y, lambda);

	% Now, costFunction is a function that takes in only one argument (the
	% neural network parameters)
	[nn_params, cost] = fmincg(costFunction, initial_nn_params, options);

	% Obtain Theta1 and Theta2 back from nn_params
	Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
					 hidden_layer_size, (input_layer_size + 1));

	Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
					 num_labels, (hidden_layer_size + 1));

	save -binary "nn_theta.dat" Theta*;
end


%%
%% Train neural network
%%
function [ Theta1 Theta2 ] = testNeuralNetwork( X, y, lambda, hidden )

	nInput	= size(X,2);	% Pitch + MFCC data
	m 		= size(X, 1);	% 
	nOutput	= max(y);		% 
	
	% theta initialization
	initial_Theta1 = randInitializeWeights(nInput, hidden);
	initial_Theta2 = randInitializeWeights(hidden, nOutput);
	initial_nn_params = [initial_Theta1(:) ; initial_Theta2(:)];	% unrolling
	
	fileName = "temp/initial_params.csv";
	if fileExists(fileName)
		initial_nn_params = load(fileName);
	else
		csvwrite(fileName, initial_nn_params);
	end

	fprintf('Test Neural Network... \n')
	
	% Call cost function
	[J, nn_params] = nnCostFunction(initial_nn_params, nInput, hidden, nOutput, X, y, lambda);
	

	% Obtain Theta1 and Theta2 back from nn_params
	Theta1 = reshape( nn_params(1:hidden * (nInput+1))      , hidden     , (nInput + 1) );
	Theta2 = reshape( nn_params((1+(hidden*(nInput+1))):end), nOutput , (hidden + 1) );
	
	% g1 = sigmoidGradient([1 -0.5 0 0.5 1]);
	% g2 = sigmoid([1 -0.5 0 0.5 1]);
	% fprintf('Sigmoid gradient evaluated at [1 -0.5 0 0.5 1]:\n');
	% fprintf('%f ', g1);
	% printf('\n');
	% fprintf('%f ', g2);
	% printf('\n');

	J
	Theta1
	Theta2
end




%% =========== Initialization =============

clear ; close all;
addpath("./funciones");
fprintf('Loading features ...\n');
data 				= load("temp/training-features.csv");		% data is [ y(i) : X(i)1 : X(i)2 : ... : X(i)n ]
[X_norm, mu, sigma] = featureNormalize(data(:,2:end));
data(:,2:end)		= X_norm;

[train test] 		= splitSamples( data, 0.8, [] );	% 
fprintf('splitted %i training, %i testing\n', size(train,1), size(test,1) );

Xtrain 		= train(:,2:end);
ytrain		= train(:,1);

Xtest		= test(:,2:end);
ytest		= test(:,1);

%%%%  /-------- testing for comparison with C# implementation
%%%% |

% data is  [ y(i) : X(i)1 : X(i)2 : ... : X(i)n ]
data 		= load("temp/training-features.csv");		
X 			= data(:,2:end);
y			= data(:,1);
testNeuralNetwork(X, y, 0, 5);

%%%% |
%%%%  \-------- end testing

if fileExists("nn_theta.dat")
	load("nn_theta.dat");
else
	[ Theta1 Theta2 ] = trainNerualNetwork( Xtrain, ytrain, 0, 100);
end

%% ================= Predict =================

[values, probability, pred] = predict(Theta1, Theta2, Xtrain);
fprintf('Training set accuracy: %f\n', mean(double(pred == ytrain)) * 100);

[values, probability, pred] = predict(Theta1, Theta2, Xtest);
fprintf('Testing  set accuracy: %f\n', mean(double(pred == ytest)) * 100);

compare = [ ytest pred values];
csvwrite("temp/predictions.csv", compare);
