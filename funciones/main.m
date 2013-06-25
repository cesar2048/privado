%% Machine Learning System

%% =========== Initialization =============

clear ; close all;

%% Setup NN parameters
addpath("./funciones");
input_layer_size  	= 303;	% 303 MFCC values + pitch values
hidden_layer_size 	= 20;	% 20 hidden units
num_labels		 	= 2;	% 6 labels, [enojado|tranquilo]
lambda 				= 1;
max_iter 			= 200;

fprintf('Loading features ...\n')
data = load("temp/features.csv");
X =  data(:,2:end);		% something like [1504 x 20]
y =  data(:,1);			% something like [1504 x 1]
m = size(X, 1);


%% ================ Part 6: Train the NN ================

fid = fopen("nn_theta.dat", "r");
if fid ~= -1
	fclose(fid);
	load("nn_theta.dat");
else
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

%% ================= Part 10: Implement Predict =================

[values, probability, pred] = predict(Theta1, Theta2, X);
fprintf('Input Set Accuracy: %f\n', mean(double(pred == y)) * 100);

compare = [ y pred values];
csvwrite("temp/predictions.csv", compare);

